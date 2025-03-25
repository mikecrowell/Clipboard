using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        public string EmailTemplateBasePath { get; set; }

        public EmailHelper(string emailTemplateBasePath)
        {
            EmailTemplateBasePath = emailTemplateBasePath;
        }

        private readonly string TOKEN_SEPARATOR = "@@@";
        private readonly Regex COMMENT_MATCHER = new Regex(@"(<!--.*-->)|(\/\*.*\*\/)");

        public EmailMessage BuildEfficiencyNavigatorRegisterEmail(string emailTo, string companyId)
        {
            string templatePath = BuildTemplatePath("EfficiencyNavigatorRegister.html");
            var replacements = new Dictionary<string, string>();
            replacements.Add(Tokenize("efficiencyNavigatorLink"), ConfigurationManager.AppSettings["efficiencyNavigatorLink"]);
            replacements.Add(Tokenize("accountId"), companyId);

            return new EmailMessage("Efficiency Navigator Registration", CreateInlineTemplate(templatePath, replacements), emailTo,
                emailFrom: ConfigurationManager.AppSettings["efficiencyNavigatorRegisterFrom"],
                emailFromDisplayName: ConfigurationManager.AppSettings["efficiencyNavigatorRegisterDisplayName"]);
        }

        private string BuildTemplatePath(string templateName)
        {
            return Path.Combine(EmailTemplateBasePath, templateName);
        }

        private string CreateInlineTemplate(string templatePath, IDictionary<string, string> replacements)
        {
            var htmlSource = File.ReadAllText(templatePath);

            // inline CSS
            ////var result = PreMailer.Net.PreMailer.MoveCssInline(htmlSource);
            ////var html = result.Html;

            // replace all keys in dictionary
            foreach (var key in replacements.Keys)
            {
                htmlSource = htmlSource.Replace(key, replacements[key]);
            }
            // remove all comments
            htmlSource = COMMENT_MATCHER.Replace(htmlSource, "");
            return htmlSource;
        }

        private string Tokenize(string token)
        {
            return String.Format("{0}{1}{0}", TOKEN_SEPARATOR, token);
        }

        public virtual async Task<bool> SendEmail(EmailMessage message)
        {
            try
            {
                MailAddress fromAddress = new MailAddress(message.emailFrom, message.emailFromDisplayName);
                MailAddress toAddress = new MailAddress(message.emailTo, message.emailTo);

                SmtpClient smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["smtpHost"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUserName"], ConfigurationManager.AppSettings["smtpPassword"])
                };

                using (MailMessage email = new MailMessage(fromAddress, toAddress) { Subject = message.emailSubject, Body = message.emailBody })
                {
                    email.Bcc.Add(fromAddress);

                    if (!string.IsNullOrWhiteSpace(message.attachmentFilePath))
                    {
                        System.Net.Mail.Attachment attachmentData = new System.Net.Mail.Attachment(message.attachmentFilePath, MediaTypeNames.Application.Octet);
                        email.Attachments.Add(attachmentData);
                    }
                    email.IsBodyHtml = true;
                    bool limitToOnlyFranklinEnergyEmailAddresses = bool.Parse(ConfigurationManager.AppSettings["limitENRegisterEmailToFranklinEnergy"]);
                    if (!limitToOnlyFranklinEnergyEmailAddresses || message.emailTo.EndsWith("@franklinenergy.com"))
                    {
                        await smtp.SendMailAsync(email);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}