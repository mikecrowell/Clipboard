using FieldTool.BLL;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using System.Environment;
using VisioForge.Types;

namespace VideoCapture
{
    public partial class frmMultimedia : DevComponents.DotNetBar.Metro.MetroForm
    {
        //string strVideoInputDevice = "";
        //string strVideoImportFormat= "";
        //bool bDebugMode =false;
        //string strAudioInputDevice = "";
        //string strAudioOutputDevice = "";
        //string strAudioInputFormat = "";
        //string strAudioInputLine = "";


        //long lImageType = 1;
        //string strJPEGQuality= "85";
        //string strPicturePath  = "";

        //string strCaptureFilename = "" ;// Return value


        public frmMultimedia()
        {
            InitializeComponent();
        }


        public string PictureFilePath { get; set; }
        public string PictureFile { get; set; }
        public ScannedItem scannedItem { get; set; }

        private string AuditId = "";
        private string EquipmentID = "";
        private bool showLoadFile = false;
        private bool isAttachment = false;
        private bool isEditMode = false;
        private string PictureDirectory = "";
        private bool gotPicture = false;


        private void frmMultimedia_Load(object sender, EventArgs e)
        {

            Clipboard.Clear();

            PictureBox1.Visible = false;
            VideoCapture1.Visible = true;

            VideoCapture1.SetLicenseKey("2011-07CD-1E46-51CD-5BF5-3025", "tlutz", "tlutz@franklinenergy.com");

            if (VideoCapture1.Video_Codecs.Count > 0 && cbVideoCodecs.Items.Count <= VideoCapture1.Video_Codecs.Count)
            {
                for (int i = 0; i < VideoCapture1.Video_Codecs.Count; ++i)
                {
                    cbVideoCodecs.Items.Add(VideoCapture1.Video_Codecs[i]);
                }
            }

            if (cbVideoCodecs.Items.Count > 0)
            {
                cbVideoCodecs.SelectedIndex = 0;
            }

            if (VideoCapture1.Audio_Codecs.Count > 0 && cbAudioCodecs.Items.Count <= VideoCapture1.Audio_Codecs.Count)
            {
                for (int i = 0; i < VideoCapture1.Audio_Codecs.Count; ++i)
                {
                    cbAudioCodecs.Items.Add(VideoCapture1.Audio_Codecs[i]);
                }
            }

            if (cbAudioCodecs.Items.Count > 0)
            {
                cbAudioCodecs.SelectedIndex = 0;
            }

            if (cbVideoCodecs.Items.IndexOf("MJPEG Compressor") != -1)
            {
                cbVideoCodecs.SelectedIndex = cbVideoCodecs.Items.IndexOf("MJPEG Compressor");
            }

            if (cbAudioCodecs.Items.IndexOf("PCM") != -1)
            {
                cbAudioCodecs.SelectedIndex = cbAudioCodecs.Items.IndexOf("PCM");
            }


            for (int i = 0; i < VideoCapture1.WMV_Internal_Profiles().Count; ++i)
            {
                cbWMVInternalProfile9.Items.Add(VideoCapture1.WMV_Internal_Profiles()[i]);

            }
            if (cbWMVInternalProfile9.Items.Count > 0)
            {
                cbWMVInternalProfile9.SelectedIndex = 0;
            }

            if (VideoCapture1.Video_CaptureDevices.Count > 0 && cbVideoInputDevice.Items.Count <= VideoCapture1.Video_CaptureDevices.Count)
            {
                for (int i = 0; i < VideoCapture1.Video_CaptureDevices.Count; ++i)
                {
                    cbVideoInputDevice.Items.Add(VideoCapture1.Video_CaptureDevices[i]);
                }
            }

            if (cbVideoInputDevice.Items.Count > 0)
            {
                bool foundRearCamera = false;
                for (int i = 0; i < cbVideoInputDevice.Items.Count; ++i)
                {
                    if (cbVideoInputDevice.Items[i].ToString().IndexOf("rear", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cbVideoInputDevice.SelectedIndex = i;
                        foundRearCamera = true;
                    }
                }
                if (!foundRearCamera)
                {
                    for (int i = 0; i < cbVideoInputDevice.Items.Count; ++i)
                    {
                        if (cbVideoInputDevice.Items[i].ToString().IndexOf("back", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbVideoInputDevice.SelectedIndex = i;
                            foundRearCamera = true;
                        }
                    }
                }
                if (!foundRearCamera)
                {
                    cbVideoInputDevice.SelectedIndex = 0;
                }
            }

            cbAudioInputDevice.Items.Clear();
            for (int i = 0; i < VideoCapture1.Audio_CaptureDevices.Count; ++i)
            {
                cbAudioInputDevice.Items.Add(VideoCapture1.Audio_CaptureDevices[i]);
            }

            cbAudioInputDevice.SelectedIndex = 0;

            cbAudioInputLine.Items.Clear();
            VideoCapture1.Audio_CaptureDevice_Lines_Fill();
            for (int i = 0; i < VideoCapture1.Audio_CaptureDevice_Lines().Count; ++i)
            {
                cbAudioInputLine.Items.Add(VideoCapture1.Audio_CaptureDevice_Lines()[i]);
            }
            if (cbAudioInputLine.Items.Count > 0)
            {
                cbAudioInputLine.SelectedIndex = 0;
            }

            if (VideoCapture1.Audio_OutputDevices != null)
            {
                for (int i = 0; i < VideoCapture1.Audio_OutputDevices.Count; ++i)
                {
                    cbAudioOutputDevice.Items.Add(VideoCapture1.Audio_OutputDevices[i]);
                }
            }


            if (cbAudioOutputDevice.Items.Count > 0)
            {
                cbAudioOutputDevice.SelectedIndex = 0;
            }


            for (int i = 0; i < VideoCapture1.Audio_Effects_Equalizer_Presets().Count; ++i)
            {
                cbAudEqualizerPreset.Items.Add(VideoCapture1.Audio_Effects_Equalizer_Presets()[i]);
            }

            cbChannels.SelectedIndex = 1;
            cbBPS.SelectedIndex = 1;
            cbImageType.SelectedIndex = 1;
            if (cbSampleRate.Items.Count > 0)
            {
                cbSampleRate.SelectedIndex = 0;
            }

            edOutput.Text = this.PictureDirectory;

            InitializeDevices();

            SetImageControlValues();

            this.SetLicenseKey();

            StartVideo();

        }

        public DialogResult ShowDialog(string filepath, bool showLoadFileControls, bool isAttachment)
        {
            this.PictureFilePath = "";
            this.PictureDirectory = filepath;
            this.showLoadFile = showLoadFileControls;
            this.isAttachment = isAttachment;

            this.SetLicenseKey();

            return base.ShowDialog();
        }

        public DialogResult ShowDialog(string filepath, bool showLoadFileControls, bool isAttachment, bool isEdit, string file)
        {
            this.PictureFilePath = "";
            this.PictureDirectory = filepath;
            this.showLoadFile = showLoadFileControls;
            this.isAttachment = isAttachment;
            this.isEditMode = isEdit;
            if (!String.IsNullOrEmpty(file))
            {
                this.PictureFile = file;
                this.PictureFilePath = Path.Combine(this.PictureDirectory, this.PictureFile);
            }

            this.SetLicenseKey();

            return base.ShowDialog();
        }

        public DialogResult ShowDialog(string Filepath, string sAuditID, string sEquipmentID)
        {

            this.PictureFilePath = "";
            this.PictureDirectory = Filepath;
            this.AuditId = sAuditID;
            this.EquipmentID = sEquipmentID;

            this.SetLicenseKey();

            return base.ShowDialog();
        }

        private void SetLicenseKey()
        {
            VideoCapture1.SetLicenseKey("2011-07CD-1E46-51CD-5BF5-3025", "tlutz", "tlutz@franklinenergy.com");
        }

        public void SetImageControlValues()
        {
            edOutput.Text = this.PictureDirectory;

            if (showLoadFile)
            {
                this.edOutput.Visible = true;
                this.btSelectOutput.Visible = true;
            }
            else
            {
                this.edOutput.Visible = false;
                this.btSelectOutput.Visible = false;
            }

            if (isEditMode)
            {
                if (File.Exists(this.PictureFilePath))
                {

                    try
                    {

                        this.edOutput.Text = this.PictureFilePath;

                        VideoCapture1.Visible = false;
                        PictureBox1.Visible = true;

                        if (PictureBox1.Image != null)
                        {
                            PictureBox1.Image.Dispose();
                        }
                        PictureBox1.Image = new Bitmap(Image.FromFile(this.PictureFilePath), 640, 480);
                        gotPicture = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    this.edOutput.ResetText();
                    gotPicture = false;
                    VideoCapture1.Visible = true;
                    PictureBox1.Visible = false;
                }
            }
            else
            {
                this.edOutput.ResetText();
                gotPicture = false;
                VideoCapture1.Visible = true;
                PictureBox1.Visible = false;
            }

        }

        public void CloseMediaForm()
        {
            this.Close();
        }

        private void cbAudioInputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAudioInputDevice.SelectedIndex != -1)
            {
                VideoCapture1.Audio_CaptureDevice = cbAudioInputDevice.Text;
                cbAudioInputFormat.Items.Clear();

                VideoCapture1.Audio_CaptureDevice_Formats_Fill();
                for (int i = 0; i < VideoCapture1.Audio_CaptureDevice_Formats().Count; ++i)
                {
                    cbAudioInputFormat.Items.Add(VideoCapture1.Audio_CaptureDevice_Formats()[i]);
                }


                if (cbAudioInputFormat.Items.Count > 0)
                {
                    cbAudioInputFormat.SelectedIndex = 0;
                }


                cbAudioInputFormat_SelectedIndexChanged(null, null);

                cbAudioInputLine.Items.Clear();

                VideoCapture1.Audio_CaptureDevice_Lines_Fill();
                for (int i = 0; i < VideoCapture1.Audio_CaptureDevice_Lines().Count; ++i)
                {
                    cbAudioInputLine.Items.Add(VideoCapture1.Audio_CaptureDevice_Lines()[i]);
                }

                if (cbAudioInputLine.Items.Count > 0)
                {
                    cbAudioInputLine.SelectedIndex = 0;
                }

                cbAudioInputLine_SelectedIndexChanged(null, null);

                btAudioInputDeviceSettings.Enabled = VideoCapture1.Audio_CaptureDevice_SettingsDialog_Exists();
            }
        }

        private void cbAudioInputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_CaptureDevice_Format = cbAudioInputFormat.Text;
        }

        private void cbAudioInputLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_CaptureDevice_Line = cbAudioInputLine.Text;
        }

        private void cbVideoCodecs_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string sName = cbVideoCodecs.Text;
            btVideoSettings.Enabled = VisioForge.Controls.VideoCapture.VideoCaptureCore.Video_Codec_Has_Dialog(sName, VFPropertyPage.Default) | VisioForge.Controls.VideoCapture.VideoCaptureCore.Audio_Codec_Has_Dialog(sName, VFPropertyPage.VFWCompConfig);
        }


        private void cbAudioCodecs_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string sName = cbAudioCodecs.Text;
            btAudioSettings.Enabled = VisioForge.Controls.VideoCapture.VideoCaptureCore.Audio_Codec_Has_Dialog(sName, VFPropertyPage.Default) | VisioForge.Controls.VideoCapture.VideoCaptureCore.Audio_Codec_Has_Dialog(sName, VFPropertyPage.VFWCompConfig);
        }


        private void cbVideoInputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbVideoInputFormat.SelectedIndex != -1)
            {
                //VideoCapture1.Video_CaptureDevice_Format = cbVideoInputFormat.Text;
            }
            else
            {
                //VideoCapture1.Video_CaptureDevice_Format = "";
                VideoCapture1.Video_CaptureDevice_Format = VFVideoCaptureOutputFormat.DirectCaptureMPEG.ToString();
            }
        }

        private void cbVideoInputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!isEditMode)
            {
                if (cbVideoInputDevice.SelectedIndex != -1)
                {
                    VideoCapture1.Video_CaptureDevice = cbVideoInputDevice.Text;

                    cbVideoInputFormat.Items.Clear();

                    VideoCapture1.Video_CaptureDevice_Formats_Fill();

                    cbVideoInputFormat.Items.Clear();
                    for (int i = 0; i < VideoCapture1.Video_CaptureDevice_Formats().Count; ++i)
                    {
                        cbVideoInputFormat.Items.Add(VideoCapture1.Video_CaptureDevice_Formats()[i]);
                    }

                    cbFramerate.Items.Clear();
                    VideoCapture1.Video_CaptureDevice_FrameRates_Fill();

                    for (int i = 0; i < VideoCapture1.Video_CaptureDevice_FrameRates().Count; ++i)
                    {
                        cbFramerate.Items.Add(VideoCapture1.Video_CaptureDevice_FrameRates()[i]);
                    }

                    cbUseAudioInputFromVideoCaptureDevice.Enabled = VideoCapture1.Video_CaptureDevice_HasInternalAudioOutput();
                    btVideoCaptureDeviceSettings.Enabled = VideoCapture1.Video_CaptureDevice_SettingsDialog_Exists();

                    VideoCapture1.Visible = false;
                    VideoCapture1.Invalidate();
                    InitializeDevices();
                    VideoCapture1.Update();
                    this.SetLicenseKey();
                    VideoCapture1.Visible = true;
                    StartVideo();
                }
            }

        }


        private void btClear_Click(object sender, EventArgs e)
        {
            ClearImageControls();
        }

        private void ClearImageControls()
        {
            VideoCapture1.SetLicenseKey("2011-07CD-1E46-51CD-5BF5-3025", "tlutz", "tlutz@franklinenergy.com");
            InitializeDevices();
            PictureBox1.Visible = false;
            edOutput.Text = "";
            isEditMode = false;
            PictureBox1.Visible = false;
            if (PictureBox1.Image != null)
            {
                PictureBox1.Image.Dispose();
            }
            VideoCapture1.Visible = true;
            gotPicture = false;
        }



        private void InitializeDevices()
        {

            VideoCapture1.Video_Renderer_Zoom_Ratio = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftX = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftY = 0;

            VideoCapture1.Debug_Mode = cbDebugMode.Checked;
            VideoCapture1.Debug_Dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VisioForge\\";

            //'apply capture parameters
            if (VisioForge.Controls.VideoCapture.VideoCaptureCore.Filter_Supported_VMR9())
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VMR9;
            }

            else
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VideoRenderer;
            }


            VideoCapture1.Video_CaptureDevice = cbVideoInputDevice.Text;
            //VideoCapture1.Video_CaptureDevice_IsAudioSource = true;
            VideoCapture1.Video_CaptureDevice_IsAudioSource = false;
            //VideoCapture1.Video_CaptureDevice_Format = cbVideoInputFormat.Text;
            VideoCapture1.Video_CaptureDevice_Format = VFVideoCaptureOutputFormat.DirectCaptureMPEG.ToString();
            //VideoCapture1.Video_CaptureDevice_Format_UseBest = cbUseBestVideoInputFormat.Checked;
            VideoCapture1.Video_CaptureDevice_Format_UseBest = false;

            if (cbFramerate.SelectedIndex != -1)
            {
                VideoCapture1.Video_CaptureDevice_FrameRate = Convert.ToSingle(Convert.ToDouble(cbFramerate.Text));
            }

            if (rbPreview.Checked)
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoPreview;
            }
            else
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoCapture;
                VideoCapture1.Output_Filename = edOutput.Text;

                if (rbAVI.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.AVI;

                    VideoCapture1.Audio_Codec_Name = cbAudioCodecs.Text;
                    VideoCapture1.Audio_Codec_Channels = Convert.ToInt32(cbChannels.Text);
                    VideoCapture1.Audio_Codec_BPS = Convert.ToInt32(cbBPS.Text);
                    VideoCapture1.Audio_Codec_SampleRate = Convert.ToInt32(cbSampleRate.Text);

                    VideoCapture1.Video_Codec = cbVideoCodecs.Text;
                }
                else if (rbWMV.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.WMV;

                    VideoCapture1.WMV_Mode = VFWMVMode.InternalProfile;

                    if (cbWMVInternalProfile9.SelectedIndex != -1)
                    {
                        VideoCapture1.WMV_Internal_Profile_Name = cbWMVInternalProfile9.Text;
                    }
                }
            }

            VideoCapture1.Video_Effects_Enabled = true;
            VideoCapture1.Video_Effects_Clear();

        }

        private void btPicture_Click(object sender, EventArgs e)
        {

            if (PictureBox1.Image != null)
            {
                PictureBox1.Image.Dispose();
            }

            PictureBox1.Image = new Bitmap(VideoCapture1.Frame_GetCurrent());

            gotPicture = true;

            VideoCapture1.Visible = false;
            PictureBox1.Visible = true;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            StartVideo();
        }

        private void StartVideo()
        {
            VideoCapture1.Video_Renderer_Zoom_Ratio = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftX = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftY = 0;

            VideoCapture1.Debug_Mode = cbDebugMode.Checked;
            VideoCapture1.Debug_Dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VisioForge\\";


            if (cbRecordAudio.Checked)
            {
                VideoCapture1.Audio_RecordAudio = true;
                VideoCapture1.Audio_PlayAudio = true;
            }
            else
            {
                VideoCapture1.Audio_RecordAudio = false;
                VideoCapture1.Audio_PlayAudio = false;
            }

            //'apply capture parameters
            if (VisioForge.Controls.VideoCapture.VideoCaptureCore.Filter_Supported_VMR9())
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VMR9;
            }
            else
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VideoRenderer;
            }

            VideoCapture1.Video_CaptureDevice = cbVideoInputDevice.Text;
            VideoCapture1.Video_CaptureDevice_IsAudioSource = true;
            VideoCapture1.Audio_OutputDevice = cbAudioOutputDevice.Text;
            //VideoCapture1.Audio_CaptureDevice_Format_UseBest = true;
            //VideoCapture1.Video_CaptureDevice_Format = cbVideoInputFormat.Text;
            //VideoCapture1.Video_CaptureDevice_Format_UseBest = cbUseBestVideoInputFormat.Checked;
            VideoCapture1.Video_CaptureDevice_Format = VFVideoCaptureOutputFormat.DirectCaptureMPEG.ToString();
            VideoCapture1.Video_CaptureDevice_Format_UseBest = false;
            VideoCapture1.Audio_CaptureDevice = cbAudioInputDevice.Text;
            VideoCapture1.Audio_CaptureDevice_Format = cbAudioInputFormat.Text;

            if (cbFramerate.SelectedIndex != -1)
            {
                VideoCapture1.Video_CaptureDevice_FrameRate = Convert.ToSingle(Convert.ToDouble(cbFramerate.Text));
            }


            if (rbPreview.Checked)
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoPreview;
            }
            else
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoCapture;
                VideoCapture1.Output_Filename = edOutput.Text;

                if (rbAVI.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.AVI;

                    VideoCapture1.Audio_Codec_Name = cbAudioCodecs.Text;
                    VideoCapture1.Audio_Codec_Channels = Convert.ToInt32(cbChannels.Text);
                    VideoCapture1.Audio_Codec_BPS = Convert.ToInt32(cbBPS.Text);
                    VideoCapture1.Audio_Codec_SampleRate = Convert.ToInt32(cbSampleRate.Text);

                    VideoCapture1.Video_Codec = cbVideoCodecs.Text;
                }
                else if (rbWMV.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.WMV;
                    VideoCapture1.WMV_Mode = VFWMVMode.InternalProfile;

                    if (cbWMVInternalProfile9.SelectedIndex != -1)
                    {
                        VideoCapture1.WMV_Internal_Profile_Name = cbWMVInternalProfile9.Text;
                    }
                }
            }

            VideoCapture1.Video_Effects_Enabled = true;
            VideoCapture1.Video_Effects_Clear();

            VideoCapture1.Video_Effects_Deinterlace_CAVT(0, 0, 0, true, 20);

            //'Audio processing
            VideoCapture1.Audio_Effects_Clear();
            VideoCapture1.Audio_Effects_Enabled = true;

            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.Amplify, cbAudAmplifyEnabled.Checked);
            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.Equalizer, cbAudEqualizerEnabled.Checked);
            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.TrueBass, cbAudTrueBassEnabled.Checked);
            VideoCapture1.Start();
        }

        public void StopVideo()
        {
            if (VideoCapture1 != null)
            {
                VideoCapture1.Stop();
            }
            //if (cbVideoInputDevice.Items.Count > 0)
            //{
            //    for (int i = 0; i < cbVideoInputDevice.Items.Count; ++i)
            //    {
            //        if (cbVideoInputDevice.Items[i].ToString().IndexOf("rear", StringComparison.OrdinalIgnoreCase) >= 0)
            //        {
            //            cbVideoInputDevice.SelectedIndex = i;
            //            if (VideoCapture1 != null)
            //            {
            //                VideoCapture1.Stop();
            //            }
            //        }
            //    }
            //}
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            //VideoCapture1.Stop();
        }

        private void btVideoCaptureDeviceSettings_Click(object sender, EventArgs e)
        {
            VideoCapture1.Video_CaptureDevice_SettingsDialog_Show();
        }

        private void btAudioInputDeviceSettings_Click(object sender, EventArgs e)
        {
            VideoCapture1.Audio_CaptureDevice_SettingsDialog_Show(IntPtr.Zero);
        }

        //private void cbVideoInputDevice_SelectedIndexChanged_1(object sender, EventArgs e) {

        //    if (cbVideoInputDevice2.SelectedIndex != -1) {
        //        VideoCapture1.Video_CaptureDevice = cbVideoInputDevice2.Text;

        //        cbVideoInputFormat.Items.Clear();

        //        VideoCapture1.Video_CaptureDevice_Formats_Fill();

        //        for (int i = 0; i < VideoCapture1.Video_CaptureDevice_Formats().Count; ++i) {
        //            cbVideoInputFormat.Items.Add(VideoCapture1.Video_CaptureDevice_Formats()[i]);
        //        }

        //        cbVideoInputFormat.SelectedIndex = 0;
        //        cbVideoInputFormat_SelectedIndexChanged(null, null);

        //        cbFramerate.Items.Clear();
        //        VideoCapture1.Video_CaptureDevice_FrameRates_Fill();

        //        for (int i = 0; i < VideoCapture1.Video_CaptureDevice_FrameRates().Count; ++i) {
        //            cbFramerate.Items.Add(VideoCapture1.Video_CaptureDevice_FrameRates()[i]);
        //        }

        //        cbFramerate.SelectedIndex = 0;

        //        cbUseAudioInputFromVideoCaptureDevice.Enabled = VideoCapture1.Video_CaptureDevice_HasInternalAudioOutput();
        //        btVideoCaptureDeviceSettings.Enabled = VideoCapture1.Video_CaptureDevice_SettingsDialog_Exists();
        //    }
        //}

        private void cbVideoInputFormat_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbFramerate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbAudioInputDevice_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbAudioInputLine_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbAudioInputFormat_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbAudioOutputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_OutputDevice = cbAudioOutputDevice.Text;
        }

        private void tbAudioVolume_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_OutputDevice_SetVolume(tbAudioVolume.Value);
        }

        private void tbAudioBalance_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_OutputDevice_SetBalance(tbAudioBalance.Value);
            VideoCapture1.Audio_OutputDevice_GetBalance();
        }

        private void btVideoSettings_Click(object sender, EventArgs e)
        {
            string sName = cbVideoCodecs.Text;

            if (VisioForge.Controls.WinForms.VideoCapture.Video_Codec_Has_Dialog(sName, VFPropertyPage.Default))
            {
                VisioForge.Controls.WinForms.VideoCapture.Video_Codec_Show_Dialog(IntPtr.Zero, sName, VFPropertyPage.Default);
            }
            else
            {
                if (VisioForge.Controls.WinForms.VideoCapture.Video_Codec_Has_Dialog(sName, VFPropertyPage.VFWCompConfig))
                {
                    VisioForge.Controls.WinForms.VideoCapture.Video_Codec_Show_Dialog(IntPtr.Zero, sName, VFPropertyPage.VFWCompConfig);
                }

            }
        }

        private void btAudioSettings_Click(object sender, EventArgs e)
        {
            string sName = cbAudioCodecs.Text;

            if (VisioForge.Controls.WinForms.VideoCapture.Audio_Codec_Has_Dialog(sName, VFPropertyPage.Default))
            {
                VisioForge.Controls.WinForms.VideoCapture.Audio_Codec_Show_Dialog(IntPtr.Zero, sName, VFPropertyPage.Default);
            }
            else
            {

                if (VisioForge.Controls.WinForms.VideoCapture.Audio_Codec_Has_Dialog(sName, VFPropertyPage.VFWCompConfig))
                {
                    VisioForge.Controls.WinForms.VideoCapture.Audio_Codec_Show_Dialog(IntPtr.Zero, sName, VFPropertyPage.VFWCompConfig);
                }
            }
        }

        private void cbVideoCodecs_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbAudioCodecs_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbBPS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSampleRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbWMVInternalProfile9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cbTextLogo_CheckedChanged(object sender, EventArgs e)
        {
            VideoCapture1.Video_Effects_Text_Logo(2, 0, 0, false, edTextLogo.Text, Convert.ToInt32(edTextLogoLeft.Text), Convert.ToInt32(edTextLogoTop.Text), fontDialog1.Font, fontDialog1.Color);
            VideoCapture1.Video_Effects_Text_Logo_Parameters_Transparency(2, tbTextLogoTransp.Value);
            VideoCapture1.Video_Effects_Text_Logo_Parameters_Update(2, cbTextLogo.Checked);
        }


        private void btFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                cbTextLogo_CheckedChanged(null, null);
            }
        }

        private void btSelectImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                edGraphicLogoFilename.Text = openFileDialog2.FileName;
            }

        }

        private void cbGraphicLogo_CheckedChanged(object sender, EventArgs e)
        {
            VideoCapture1.Video_Effects_Picture_Logo_Ex(edGraphicLogoFilename.Text, 1, 0, 0, cbGraphicLogo.Checked, Convert.ToUInt32(edGraphicLogoLeft.Text), Convert.ToUInt32(edGraphicLogoTop.Text), null, tbGraphicLogoTransp.Value, Color.Black, false);
        }

        private void tbGraphicLogoTransp_Scroll(object sender, EventArgs e)
        {
            cbGraphicLogo_CheckedChanged(null, null);
        }

        private void tbTextLogoTransp_Scroll(object sender, EventArgs e)
        {
            cbTextLogo_CheckedChanged(null, null);
        }

        private void cbAudAmplifyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Enable(0, cbAudAmplifyEnabled.Checked);
        }

        private void tbAudAmplifyAmp_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Amplify(0, tbAudAmplifyAmp.Value * 10, false);
        }

        private void tbAudEq0_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 0, tbAudEq0.Value);
        }

        private void tbAudEq1_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 1, tbAudEq1.Value);
        }

        private void tbAudEq2_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 2, tbAudEq2.Value);
        }

        private void tbAudEq4_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 4, tbAudEq4.Value);
        }

        private void tbAudEq6_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 6, tbAudEq6.Value);
        }

        private void tbAudEq3_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 3, tbAudEq3.Value);
        }

        private void tbAudEq5_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 5, tbAudEq5.Value);
        }

        private void tbAudEq7_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 7, tbAudEq7.Value);
        }

        private void tbAudEq8_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 8, tbAudEq8.Value);
        }

        private void tbAudEq9_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Band_Set(1, 9, tbAudEq9.Value);
        }

        private void cbAudEqualizerPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Equalizer_Preset_Set(1, cbAudEqualizerPreset.Text);
            btAudEqRefresh_Click(sender, e);
        }
        private void btAudEqRefresh_Click(object sender, EventArgs e)
        {
            tbAudEq0.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 0);
            tbAudEq1.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 1);
            tbAudEq2.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 2);
            tbAudEq3.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 3);
            tbAudEq4.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 4);
            tbAudEq5.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 5);
            tbAudEq6.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 6);
            tbAudEq7.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 7);
            tbAudEq8.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 8);
            tbAudEq9.Value = VideoCapture1.Audio_Effects_Equalizer_Band_Get(1, 9);
        }

        private void btAudEqRefresh_Click_1(object sender, EventArgs e)
        {

        }

        private void cbAudTrueBassEnabled_CheckedChanged(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_Enable(5, cbAudTrueBassEnabled.Checked);
        }

        private void tbAudTrueBass_Scroll(object sender, EventArgs e)
        {
            VideoCapture1.Audio_Effects_TrueBass(5, 200, false, tbAudTrueBass.Value);
        }

        private void edOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btSelectOutput_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(PictureDirectory))
            {
                this.saveFileDialog1.InitialDirectory = PictureDirectory;
            }
            else
            {
                Directory.CreateDirectory(PictureDirectory);
                this.saveFileDialog1.InitialDirectory = PictureDirectory;
            }

            this.saveFileDialog1.OverwritePrompt = false;
            this.saveFileDialog1.Title = "Select Image File";
            this.saveFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.gif)|*.bmp;*.jpg;*.jpeg;*.gif|All files (*.*)|*.*";
            this.saveFileDialog1.CheckFileExists = true;

            DialogResult result = this.saveFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && File.Exists(this.saveFileDialog1.FileName))
            {

                try
                {

                    if (PictureBox1.Image != null)
                    {
                        PictureBox1.Image.Dispose();
                    }

                    PictureBox1.Image = new Bitmap(this.saveFileDialog1.FileName);

                    this.edOutput.Text = this.saveFileDialog1.FileName;

                    gotPicture = true;

                    this.PictureFile = Path.GetFileName(this.saveFileDialog1.FileName);

                    VideoCapture1.Visible = false;
                    PictureBox1.Visible = true;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("An incompatible file format was selected.  Please select a different file.");
                }

            }
        }

        private void rbCapture_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btStart_Click_1(object sender, EventArgs e)
        {
            VideoCapture1.Video_Renderer_Zoom_Ratio = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftX = 0;
            VideoCapture1.Video_Renderer_Zoom_ShiftY = 0;

            VideoCapture1.Debug_Mode = cbDebugMode.Checked;
            VideoCapture1.Debug_Dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VisioForge\\";


            if (cbRecordAudio.Checked)
            {
                VideoCapture1.Audio_RecordAudio = true;
                VideoCapture1.Audio_PlayAudio = true;
            }
            else
            {
                VideoCapture1.Audio_RecordAudio = false;
                VideoCapture1.Audio_PlayAudio = false;
            }

            //'apply capture parameters
            if (VisioForge.Controls.VideoCapture.VideoCaptureCore.Filter_Supported_VMR9())
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VMR9;
            }
            else
            {
                VideoCapture1.Video_Renderer = VFVideoRenderer.VideoRenderer;
            }

            VideoCapture1.Video_CaptureDevice = cbVideoInputDevice.Text;
            VideoCapture1.Video_CaptureDevice_IsAudioSource = true;
            VideoCapture1.Audio_OutputDevice = cbAudioOutputDevice.Text;
            //VideoCapture1.Audio_CaptureDevice_Format_UseBest = true;
            //VideoCapture1.Video_CaptureDevice_Format = cbVideoInputFormat.Text;
            //VideoCapture1.Video_CaptureDevice_Format_UseBest = cbUseBestVideoInputFormat.Checked;
            VideoCapture1.Video_CaptureDevice_Format = VFVideoCaptureOutputFormat.DirectCaptureMPEG.ToString();
            VideoCapture1.Video_CaptureDevice_Format_UseBest = false;
            VideoCapture1.Audio_CaptureDevice = cbAudioInputDevice.Text;
            VideoCapture1.Audio_CaptureDevice_Format = cbAudioInputFormat.Text;

            if (cbFramerate.SelectedIndex != -1)
            {
                VideoCapture1.Video_CaptureDevice_FrameRate = Convert.ToSingle(Convert.ToDouble(cbFramerate.Text));
            }


            if (rbPreview.Checked)
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoPreview;
            }
            else
            {
                VideoCapture1.Mode = VFVideoCaptureMode.VideoCapture;
                VideoCapture1.Output_Filename = edOutput.Text;

                if (rbAVI.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.AVI;

                    VideoCapture1.Audio_Codec_Name = cbAudioCodecs.Text;
                    VideoCapture1.Audio_Codec_Channels = Convert.ToInt32(cbChannels.Text);
                    VideoCapture1.Audio_Codec_BPS = Convert.ToInt32(cbBPS.Text);
                    VideoCapture1.Audio_Codec_SampleRate = Convert.ToInt32(cbSampleRate.Text);

                    VideoCapture1.Video_Codec = cbVideoCodecs.Text;
                }
                else if (rbWMV.Checked)
                {
                    VideoCapture1.Output_Format = VFVideoCaptureOutputFormat.WMV;
                    VideoCapture1.WMV_Mode = VFWMVMode.InternalProfile;

                    if (cbWMVInternalProfile9.SelectedIndex != -1)
                    {
                        VideoCapture1.WMV_Internal_Profile_Name = cbWMVInternalProfile9.Text;
                    }
                }
            }

            VideoCapture1.Video_Effects_Enabled = true;
            VideoCapture1.Video_Effects_Clear();

            VideoCapture1.Video_Effects_Deinterlace_CAVT(0, 0, 0, true, 20);

            //'Audio processing
            VideoCapture1.Audio_Effects_Clear();
            VideoCapture1.Audio_Effects_Enabled = true;

            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.Amplify, cbAudAmplifyEnabled.Checked);
            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.Equalizer, cbAudEqualizerEnabled.Checked);
            VideoCapture1.Audio_Effects_Add(VFAudioEffectType.TrueBass, cbAudTrueBassEnabled.Checked);

            VideoCapture1.Start();

        }



        //private void btVideoCaptureDeviceSettings_Click(object sender, EventArgs e)
        //{
        //    VideoCapture1.Video_CaptureDevice_SettingsDialog_Show();
        //}

        //private void btAudioInputDeviceSettings_Click(object sender, EventArgs e)
        //{
        //    VideoCapture1.Audio_CaptureDevice_SettingsDialog_Show(IntPtr.Zero);
        //}

        //private void cbVideoInputDevice_SelectedIndexChanged_1(object sender, EventArgs e)
        //{

        //    if (cbVideoInputDevice.SelectedIndex != -1)
        //    {
        //        VideoCapture1.Video_CaptureDevice = cbVideoInputDevice.Text;

        //        cbVideoInputFormat.Items.Clear();

        //        VideoCapture1.Video_CaptureDevice_Formats_Fill();

        //        for (int i = 0; i < VideoCapture1.Video_CaptureDevice_Formats().Count; ++i)
        //        {
        //            cbVideoInputFormat.Items.Add(VideoCapture1.Video_CaptureDevice_Formats()[i]);
        //        }

        //        cbVideoInputFormat.SelectedIndex = 0;
        //        cbVideoInputFormat_SelectedIndexChanged(null, null);

        //        cbFramerate.Items.Clear();
        //        VideoCapture1.Video_CaptureDevice_FrameRates_Fill();

        //        for (int i = 0; i < VideoCapture1.Video_CaptureDevice_FrameRates().Count; ++i)
        //        {
        //            cbFramerate.Items.Add(VideoCapture1.Video_CaptureDevice_FrameRates()[i]);
        //        }

        //        cbFramerate.SelectedIndex = 0;

        //        cbUseAudioInputFromVideoCaptureDevice.Enabled = VideoCapture1.Video_CaptureDevice_HasInternalAudioOutput();
        //        btVideoCaptureDeviceSettings.Enabled = VideoCapture1.Video_CaptureDevice_SettingsDialog_Exists();
        //    }
        //}

        private void btStop_Click_1(object sender, EventArgs e)
        {
            //VideoCapture1.Stop();
        }

        private void btStop_Click_2(object sender, EventArgs e)
        {
            //VideoCapture1.Stop();
        }

        private void ClearLists()
        {
            cbVideoCodecs.Items.Clear();
            cbAudioCodecs.Items.Clear();
            cbWMVInternalProfile9.Items.Clear();
            cbVideoInputDevice.Items.Clear();
            cbAudioInputDevice.Items.Clear();
            cbAudioInputLine.Items.Clear();
            cbAudioOutputDevice.Items.Clear();
            cbAudEqualizerPreset.Items.Clear();
        }

        private void btnSaveExit_Click(object sender, EventArgs e)
        {
            string sImageFilepath = "";
            string sFileName = "";

            if (gotPicture)
            {
                Random r = new Random();
                int filenum = r.Next();

                if (!Directory.Exists(this.PictureDirectory))
                {
                    Directory.CreateDirectory(this.PictureDirectory);
                }

                if (!String.IsNullOrEmpty(edOutput.Text))
                {
                    if (this.PictureDirectory.Equals(Path.GetDirectoryName(edOutput.Text)))
                    {
                        sImageFilepath = edOutput.Text;
                        sFileName = PictureFile;
                    }

                }

                if (!File.Exists(sImageFilepath))
                {
                    if (isAttachment)
                    {
                        sFileName = "Attachmentpic" + filenum + ".jpg";
                    }
                    else
                    {
                        sFileName = "Equipmentpic" + filenum + ".jpg";
                    }

                    sImageFilepath = Path.Combine(this.PictureDirectory, sFileName);

                    if (PictureBox1.Image != null)
                    {

                        FileStream fstream = new FileStream(sImageFilepath, FileMode.Create);

                        PictureBox1.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        fstream.Close();
                        PictureBox1.Image.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("There are no pictures to save.");
                    }
                }

                if (!isAttachment)
                {
                    Audit a = DataStore.GetAudit(this.AuditId);

                    if (File.Exists(sImageFilepath))
                    {
                        foreach (Building b in a.Buildings)
                        {
                            foreach (EquipmentMaster em in b.Equipments)
                            {
                                if (em.ComponentId == this.EquipmentID)
                                {
                                    em.ImageFilePath = sImageFilepath;
                                    DataStore.SaveData();
                                }

                            }

                        }
                    }
                }
                this.PictureFilePath = sImageFilepath;
                this.PictureFile = sFileName;

                ClearLists();
                PictureBox1.Visible = false;
                if (PictureBox1.Image != null)
                {
                    PictureBox1.Image.Dispose();
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("There are no pictures to save.");
            }
        }

        private void btnCancelAndExit_Click(object sender, EventArgs e)
        {
            this.PictureFilePath = "";
            this.PictureFile = "";
            ClearLists();
            PictureBox1.Visible = false;
            if (PictureBox1.Image != null)
            {
                PictureBox1.Image.Dispose();
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void RemoveHandlers()
        {
            this.cbVideoInputDevice.SelectedIndexChanged -= this.cbVideoInputDevice_SelectedIndexChanged;
            this.btStop.Click -= this.btStop_Click_2;
            this.btStart.Click -= this.btStart_Click_1;
            this.btnCancelAndExit.Click -= this.btnCancelAndExit_Click;
            this.btnSaveExit.Click -= this.btnSaveExit_Click;
            this.btClear.Click -= this.btClear_Click;
            this.btPicture.Click -= this.btPicture_Click;
            this.btSelectOutput.Click -= this.btSelectOutput_Click;
            this.FormClosing -= this.frmMultimedia_FormClosing;
            this.Load -= this.frmMultimedia_Load;
        }

        private void frmMultimedia_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

    }
}



