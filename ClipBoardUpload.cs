

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "FieldTool.UI\App.config"
//     Connection String Name: "ClipBoardUpload"
//     Connection String:      "Server=tcp:d27gj4yn74.database.windows.net,1433;Database=fes-dev-cb-lookup;User ID=franklinadmin@d27gj4yn74;password=**zapped**;"

// Database Edition: SQL Azure
// Database Engine Edition: Azure

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.50
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace FieldTool.Entity
{
    // ************************************************************************
    // Unit of work
    public interface IClipBoardUpload : IDisposable
    {
        DbSet<AuditProject> AuditProjects { get; set; } // AuditProject
        DbSet<AuditProjectReport> AuditProjectReports { get; set; } // AuditProjectReport
        DbSet<AuditUploadBackup> AuditUploadBackups { get; set; } // AuditUploadBackup
        DbSet<Building> Buildings { get; set; } // Building
        DbSet<BuildingAttachment> BuildingAttachments { get; set; } // BuildingAttachment
        DbSet<BuildingElectricHistory> BuildingElectricHistories { get; set; } // BuildingElectricHistory
        DbSet<BuildingEquipment> BuildingEquipments { get; set; } // BuildingEquipment
        DbSet<BuildingEquipmentSchedule> BuildingEquipmentSchedules { get; set; } // BuildingEquipmentSchedule
        DbSet<BuildingEquipmentScheduleDuration> BuildingEquipmentScheduleDurations { get; set; } // BuildingEquipmentScheduleDuration
        DbSet<BuildingGasHistory> BuildingGasHistories { get; set; } // BuildingGasHistory
        DbSet<BuildingSpace> BuildingSpaces { get; set; } // BuildingSpace
        DbSet<BuildingType> BuildingTypes { get; set; } // BuildingType
        DbSet<BuildingUnitType> BuildingUnitTypes { get; set; } // BuildingUnitType
        DbSet<Company> Companies { get; set; } // Company
        DbSet<Contact> Contacts { get; set; } // Contact
        DbSet<DiAccount> DiAccounts { get; set; } // DIAccount
        DbSet<DiAnswer> DiAnswers { get; set; } // DIAnswer
        DbSet<DiAttachment> DiAttachments { get; set; } // DIAttachment
        DbSet<DiContact> DiContacts { get; set; } // DIContact
        DbSet<DiElectricHistory> DiElectricHistories { get; set; } // DIElectricHistory
        DbSet<DiGasHistory> DiGasHistories { get; set; } // DIGasHistory
        DbSet<DiInkSecureSignatureData> DiInkSecureSignatureDatas { get; set; } // DIInkSecureSignatureData
        DbSet<DiProjectInfo> DiProjectInfoes { get; set; } // DIProjectInfo
        DbSet<DiRetrofit> DiRetrofits { get; set; } // DIRetrofit
        DbSet<DiUploadBackup> DiUploadBackups { get; set; } // DIUploadBackup
        DbSet<EmailRequestLog> EmailRequestLogs { get; set; } // EmailRequestLog
        DbSet<InkSecureSignatureData> InkSecureSignatureData { get; set; } // InkSecureSignatureData
        DbSet<MultiFamily> MultiFamilies { get; set; } // MultiFamily
        DbSet<Recommendation> Recommendations { get; set; } // Recommendation
        DbSet<RecommendationOption> RecommendationOptions { get; set; } // RecommendationOption
        DbSet<RecommendationOptionEquipment> RecommendationOptionEquipments { get; set; } // RecommendationOptionEquipment
        DbSet<ReportTracking> ReportTrackings { get; set; } // ReportTracking
        DbSet<Retrofit> Retrofits { get; set; } // Retrofit
        DbSet<RetrofitEstimate> RetrofitEstimates { get; set; } // RetrofitEstimate
        DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; } // database_firewall_rules
        DbSet<sys_ScriptDeployment> sys_ScriptDeployments { get; set; } // script_deployments
        DbSet<sys_ScriptDeploymentStatus> sys_ScriptDeploymentStatus { get; set; } // script_deployment_status
        DbSet<UploadedFile> UploadedFiles { get; set; } // UploadedFile

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        // Stored Procedures
    }

    // ************************************************************************
    // Database context
    public partial class ClipBoardUpload : DbContext, IClipBoardUpload
    {
        public DbSet<AuditProject> AuditProjects { get; set; } // AuditProject
        public DbSet<AuditProjectReport> AuditProjectReports { get; set; } // AuditProjectReport
        public DbSet<AuditUploadBackup> AuditUploadBackups { get; set; } // AuditUploadBackup
        public DbSet<Building> Buildings { get; set; } // Building
        public DbSet<BuildingAttachment> BuildingAttachments { get; set; } // BuildingAttachment
        public DbSet<BuildingElectricHistory> BuildingElectricHistories { get; set; } // BuildingElectricHistory
        public DbSet<BuildingEquipment> BuildingEquipments { get; set; } // BuildingEquipment
        public DbSet<BuildingEquipmentSchedule> BuildingEquipmentSchedules { get; set; } // BuildingEquipmentSchedule
        public DbSet<BuildingEquipmentScheduleDuration> BuildingEquipmentScheduleDurations { get; set; } // BuildingEquipmentScheduleDuration
        public DbSet<BuildingGasHistory> BuildingGasHistories { get; set; } // BuildingGasHistory
        public DbSet<BuildingSpace> BuildingSpaces { get; set; } // BuildingSpace
        public DbSet<BuildingType> BuildingTypes { get; set; } // BuildingType
        public DbSet<BuildingUnitType> BuildingUnitTypes { get; set; } // BuildingUnitType
        public DbSet<Company> Companies { get; set; } // Company
        public DbSet<Contact> Contacts { get; set; } // Contact
        public DbSet<DiAccount> DiAccounts { get; set; } // DIAccount
        public DbSet<DiAnswer> DiAnswers { get; set; } // DIAnswer
        public DbSet<DiAttachment> DiAttachments { get; set; } // DIAttachment
        public DbSet<DiContact> DiContacts { get; set; } // DIContact
        public DbSet<DiElectricHistory> DiElectricHistories { get; set; } // DIElectricHistory
        public DbSet<DiGasHistory> DiGasHistories { get; set; } // DIGasHistory
        public DbSet<DiInkSecureSignatureData> DiInkSecureSignatureDatas { get; set; } // DIInkSecureSignatureData
        public DbSet<DiProjectInfo> DiProjectInfoes { get; set; } // DIProjectInfo
        public DbSet<DiRetrofit> DiRetrofits { get; set; } // DIRetrofit
        public DbSet<DiUploadBackup> DiUploadBackups { get; set; } // DIUploadBackup
        public DbSet<EmailRequestLog> EmailRequestLogs { get; set; } // EmailRequestLog
        public DbSet<InkSecureSignatureData> InkSecureSignatureData { get; set; } // InkSecureSignatureData
        public DbSet<MultiFamily> MultiFamilies { get; set; } // MultiFamily
        public DbSet<Recommendation> Recommendations { get; set; } // Recommendation
        public DbSet<RecommendationOption> RecommendationOptions { get; set; } // RecommendationOption
        public DbSet<RecommendationOptionEquipment> RecommendationOptionEquipments { get; set; } // RecommendationOptionEquipment
        public DbSet<ReportTracking> ReportTrackings { get; set; } // ReportTracking
        public DbSet<Retrofit> Retrofits { get; set; } // Retrofit
        public DbSet<RetrofitEstimate> RetrofitEstimates { get; set; } // RetrofitEstimate
        public DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; } // database_firewall_rules
        public DbSet<sys_ScriptDeployment> sys_ScriptDeployments { get; set; } // script_deployments
        public DbSet<sys_ScriptDeploymentStatus> sys_ScriptDeploymentStatus { get; set; } // script_deployment_status
        public DbSet<UploadedFile> UploadedFiles { get; set; } // UploadedFile

        static ClipBoardUpload()
        {
            System.Data.Entity.Database.SetInitializer<ClipBoardUpload>(null);
        }

        public ClipBoardUpload()
            : base("Name=ClipBoardUpload")
        {
            InitializePartial();
        }

        public ClipBoardUpload(string connectionString) : base(connectionString)
        {
            InitializePartial();
        }

        public ClipBoardUpload(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AuditProjectMap());
            modelBuilder.Configurations.Add(new AuditProjectReportMap());
            modelBuilder.Configurations.Add(new AuditUploadBackupMap());
            modelBuilder.Configurations.Add(new BuildingMap());
            modelBuilder.Configurations.Add(new BuildingAttachmentMap());
            modelBuilder.Configurations.Add(new BuildingElectricHistoryMap());
            modelBuilder.Configurations.Add(new BuildingEquipmentMap());
            modelBuilder.Configurations.Add(new BuildingEquipmentScheduleMap());
            modelBuilder.Configurations.Add(new BuildingEquipmentScheduleDurationMap());
            modelBuilder.Configurations.Add(new BuildingGasHistoryMap());
            modelBuilder.Configurations.Add(new BuildingSpaceMap());
            modelBuilder.Configurations.Add(new BuildingTypeMap());
            modelBuilder.Configurations.Add(new BuildingUnitTypeMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new DiAccountMap());
            modelBuilder.Configurations.Add(new DiAnswerMap());
            modelBuilder.Configurations.Add(new DiAttachmentMap());
            modelBuilder.Configurations.Add(new DiContactMap());
            modelBuilder.Configurations.Add(new DiElectricHistoryMap());
            modelBuilder.Configurations.Add(new DiGasHistoryMap());
            modelBuilder.Configurations.Add(new DiInkSecureSignatureDataMap());
            modelBuilder.Configurations.Add(new DiProjectInfoMap());
            modelBuilder.Configurations.Add(new DiRetrofitMap());
            modelBuilder.Configurations.Add(new DiUploadBackupMap());
            modelBuilder.Configurations.Add(new EmailRequestLogMap());
            modelBuilder.Configurations.Add(new InkSecureSignatureDataMap());
            modelBuilder.Configurations.Add(new MultiFamilyMap());
            modelBuilder.Configurations.Add(new RecommendationMap());
            modelBuilder.Configurations.Add(new RecommendationOptionMap());
            modelBuilder.Configurations.Add(new RecommendationOptionEquipmentMap());
            modelBuilder.Configurations.Add(new ReportTrackingMap());
            modelBuilder.Configurations.Add(new RetrofitMap());
            modelBuilder.Configurations.Add(new RetrofitEstimateMap());
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleMap());
            modelBuilder.Configurations.Add(new sys_ScriptDeploymentMap());
            modelBuilder.Configurations.Add(new sys_ScriptDeploymentStatusMap());
            modelBuilder.Configurations.Add(new UploadedFileMap());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AuditProjectMap(schema));
            modelBuilder.Configurations.Add(new AuditProjectReportMap(schema));
            modelBuilder.Configurations.Add(new AuditUploadBackupMap(schema));
            modelBuilder.Configurations.Add(new BuildingMap(schema));
            modelBuilder.Configurations.Add(new BuildingAttachmentMap(schema));
            modelBuilder.Configurations.Add(new BuildingElectricHistoryMap(schema));
            modelBuilder.Configurations.Add(new BuildingEquipmentMap(schema));
            modelBuilder.Configurations.Add(new BuildingEquipmentScheduleMap(schema));
            modelBuilder.Configurations.Add(new BuildingEquipmentScheduleDurationMap(schema));
            modelBuilder.Configurations.Add(new BuildingGasHistoryMap(schema));
            modelBuilder.Configurations.Add(new BuildingSpaceMap(schema));
            modelBuilder.Configurations.Add(new BuildingTypeMap(schema));
            modelBuilder.Configurations.Add(new BuildingUnitTypeMap(schema));
            modelBuilder.Configurations.Add(new CompanyMap(schema));
            modelBuilder.Configurations.Add(new ContactMap(schema));
            modelBuilder.Configurations.Add(new DiAccountMap(schema));
            modelBuilder.Configurations.Add(new DiAnswerMap(schema));
            modelBuilder.Configurations.Add(new DiAttachmentMap(schema));
            modelBuilder.Configurations.Add(new DiContactMap(schema));
            modelBuilder.Configurations.Add(new DiElectricHistoryMap(schema));
            modelBuilder.Configurations.Add(new DiGasHistoryMap(schema));
            modelBuilder.Configurations.Add(new DiInkSecureSignatureDataMap(schema));
            modelBuilder.Configurations.Add(new DiProjectInfoMap(schema));
            modelBuilder.Configurations.Add(new DiRetrofitMap(schema));
            modelBuilder.Configurations.Add(new DiUploadBackupMap(schema));
            modelBuilder.Configurations.Add(new EmailRequestLogMap(schema));
            modelBuilder.Configurations.Add(new InkSecureSignatureDataMap(schema));
            modelBuilder.Configurations.Add(new MultiFamilyMap(schema));
            modelBuilder.Configurations.Add(new RecommendationMap(schema));
            modelBuilder.Configurations.Add(new RecommendationOptionMap(schema));
            modelBuilder.Configurations.Add(new RecommendationOptionEquipmentMap(schema));
            modelBuilder.Configurations.Add(new ReportTrackingMap(schema));
            modelBuilder.Configurations.Add(new RetrofitMap(schema));
            modelBuilder.Configurations.Add(new RetrofitEstimateMap(schema));
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleMap(schema));
            modelBuilder.Configurations.Add(new sys_ScriptDeploymentMap(schema));
            modelBuilder.Configurations.Add(new sys_ScriptDeploymentStatusMap(schema));
            modelBuilder.Configurations.Add(new UploadedFileMap(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);

        // Stored Procedures
    }

    // ************************************************************************
    // Fake Database context
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class FakeClipBoardUpload : IClipBoardUpload
    {
        public DbSet<AuditProject> AuditProjects { get; set; }
        public DbSet<AuditProjectReport> AuditProjectReports { get; set; }
        public DbSet<AuditUploadBackup> AuditUploadBackups { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingAttachment> BuildingAttachments { get; set; }
        public DbSet<BuildingElectricHistory> BuildingElectricHistories { get; set; }
        public DbSet<BuildingEquipment> BuildingEquipments { get; set; }
        public DbSet<BuildingEquipmentSchedule> BuildingEquipmentSchedules { get; set; }
        public DbSet<BuildingEquipmentScheduleDuration> BuildingEquipmentScheduleDurations { get; set; }
        public DbSet<BuildingGasHistory> BuildingGasHistories { get; set; }
        public DbSet<BuildingSpace> BuildingSpaces { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<BuildingUnitType> BuildingUnitTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DiAccount> DiAccounts { get; set; }
        public DbSet<DiAnswer> DiAnswers { get; set; }
        public DbSet<DiAttachment> DiAttachments { get; set; }
        public DbSet<DiContact> DiContacts { get; set; }
        public DbSet<DiElectricHistory> DiElectricHistories { get; set; }
        public DbSet<DiGasHistory> DiGasHistories { get; set; }
        public DbSet<DiInkSecureSignatureData> DiInkSecureSignatureDatas { get; set; }
        public DbSet<DiProjectInfo> DiProjectInfoes { get; set; }
        public DbSet<DiRetrofit> DiRetrofits { get; set; }
        public DbSet<DiUploadBackup> DiUploadBackups { get; set; }
        public DbSet<EmailRequestLog> EmailRequestLogs { get; set; }
        public DbSet<InkSecureSignatureData> InkSecureSignatureData { get; set; }
        public DbSet<MultiFamily> MultiFamilies { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<RecommendationOption> RecommendationOptions { get; set; }
        public DbSet<RecommendationOptionEquipment> RecommendationOptionEquipments { get; set; }
        public DbSet<ReportTracking> ReportTrackings { get; set; }
        public DbSet<Retrofit> Retrofits { get; set; }
        public DbSet<RetrofitEstimate> RetrofitEstimates { get; set; }
        public DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; }
        public DbSet<sys_ScriptDeployment> sys_ScriptDeployments { get; set; }
        public DbSet<sys_ScriptDeploymentStatus> sys_ScriptDeploymentStatus { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public FakeClipBoardUpload()
        {
            AuditProjects = new FakeDbSet<AuditProject>();
            AuditProjectReports = new FakeDbSet<AuditProjectReport>();
            AuditUploadBackups = new FakeDbSet<AuditUploadBackup>();
            Buildings = new FakeDbSet<Building>();
            BuildingAttachments = new FakeDbSet<BuildingAttachment>();
            BuildingElectricHistories = new FakeDbSet<BuildingElectricHistory>();
            BuildingEquipments = new FakeDbSet<BuildingEquipment>();
            BuildingEquipmentSchedules = new FakeDbSet<BuildingEquipmentSchedule>();
            BuildingEquipmentScheduleDurations = new FakeDbSet<BuildingEquipmentScheduleDuration>();
            BuildingGasHistories = new FakeDbSet<BuildingGasHistory>();
            BuildingSpaces = new FakeDbSet<BuildingSpace>();
            BuildingTypes = new FakeDbSet<BuildingType>();
            BuildingUnitTypes = new FakeDbSet<BuildingUnitType>();
            Companies = new FakeDbSet<Company>();
            Contacts = new FakeDbSet<Contact>();
            DiAccounts = new FakeDbSet<DiAccount>();
            DiAnswers = new FakeDbSet<DiAnswer>();
            DiAttachments = new FakeDbSet<DiAttachment>();
            DiContacts = new FakeDbSet<DiContact>();
            DiElectricHistories = new FakeDbSet<DiElectricHistory>();
            DiGasHistories = new FakeDbSet<DiGasHistory>();
            DiInkSecureSignatureDatas = new FakeDbSet<DiInkSecureSignatureData>();
            DiProjectInfoes = new FakeDbSet<DiProjectInfo>();
            DiRetrofits = new FakeDbSet<DiRetrofit>();
            DiUploadBackups = new FakeDbSet<DiUploadBackup>();
            EmailRequestLogs = new FakeDbSet<EmailRequestLog>();
            InkSecureSignatureData = new FakeDbSet<InkSecureSignatureData>();
            MultiFamilies = new FakeDbSet<MultiFamily>();
            Recommendations = new FakeDbSet<Recommendation>();
            RecommendationOptions = new FakeDbSet<RecommendationOption>();
            RecommendationOptionEquipments = new FakeDbSet<RecommendationOptionEquipment>();
            ReportTrackings = new FakeDbSet<ReportTracking>();
            Retrofits = new FakeDbSet<Retrofit>();
            RetrofitEstimates = new FakeDbSet<RetrofitEstimate>();
            sys_DatabaseFirewallRules = new FakeDbSet<sys_DatabaseFirewallRule>();
            sys_ScriptDeployments = new FakeDbSet<sys_ScriptDeployment>();
            sys_ScriptDeploymentStatus = new FakeDbSet<sys_ScriptDeploymentStatus>();
            UploadedFiles = new FakeDbSet<UploadedFile>();
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            ++SaveChangesCount;
            return 1;
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        // Stored Procedures
    }

    // ************************************************************************
    // Fake DbSet
    // Implementing Find:
    //      The Find method is difficult to implement in a generic fashion. If
    //      you need to test code that makes use of the Find method it is
    //      easiest to create a test DbSet for each of the entity types that
    //      need to support find. You can then write logic to find that
    //      particular type of entity, as shown below:
    //      public class FakeBlogDbSet : FakeDbSet<Blog>
    //      {
    //          public override Blog Find(params object[] keyValues)
    //          {
    //              var id = (int) keyValues.Single();
    //              return this.SingleOrDefault(b => b.BlogId == id);
    //          }
    //      }
    //      Read more about it here: https://msdn.microsoft.com/en-us/data/dn314431.aspx
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class FakeDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
        where TEntity : class
    {
        private readonly ObservableCollection<TEntity> _data;
        private readonly IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            var items = entities.ToList();
            foreach (var entity in items)
            {
                _data.Add(entity);
            }
            return items;
        }

        public override TEntity Add(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }
    }

    internal class FakeDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal FakeDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new FakeDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new FakeDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public System.Threading.Tasks.Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute(expression));
        }

        public System.Threading.Tasks.Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class FakeDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public FakeDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public FakeDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new FakeDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new FakeDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class FakeDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public FakeDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public System.Threading.Tasks.Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }

    // ************************************************************************
    // POCO classes

    // AuditProject
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class AuditProject
    {
        public string AuditProjectBsid { get; set; } // AuditProjectBSID (Primary key)
        public string CompanyBsid { get; set; } // CompanyBSID
        public string ProgramId { get; set; } // ProgramID
        public string AuditProjectName { get; set; } // AuditProjectName
        public string AuditProjectDescription { get; set; } // AuditProjectDescription
        public DateTime? ScheduledStart { get; set; } // ScheduledStart
        public DateTime? ScheduledEnd { get; set; } // ScheduledEnd
        public string CompanyContact { get; set; } // CompanyContact
        public string AuditStatus { get; set; } // AuditStatus
        public DateTime? AuditCompleteDate { get; set; } // AuditCompleteDate
        public string EnergyAdvisorName { get; set; } // EnergyAdvisorName
        public string ClientAccountId { get; set; } // ClientAccountId
        public string ElectricDisplayAs { get; set; } // ElectricDisplayAs
        public string GasDisplayAs { get; set; } // GasDisplayAs
        public bool IsAdHocAudit { get; set; } // IsAdHocAudit
        public bool IsReportEmailSent { get; set; } // IsReportEmailSent

        // Reverse navigation
        public virtual ICollection<AuditProjectReport> AuditProjectReports { get; set; } // AuditProjectReport.FK_AuditProjectReport_AuditProject
        public virtual ICollection<Building> Buildings { get; set; } // Building.FK_Building_AuditProject
        public virtual ICollection<Recommendation> Recommendations { get; set; } // Recommendation.FK_Recommendation_AuditProject
        public virtual ICollection<Retrofit> Retrofits { get; set; } // Retrofit.FK_Retrofit_AuditProject
        public virtual InkSecureSignatureData InkSecureSignatureData { get; set; } // InkSecureSignatureData.FK_InkSecureSignatureData_AuditProject

        // Foreign keys
        public virtual Company Company { get; set; } // FK_AuditProject_Company

        public AuditProject()
        {
            IsAdHocAudit = false;
            IsReportEmailSent = false;
            AuditProjectReports = new List<AuditProjectReport>();
            Buildings = new List<Building>();
            Recommendations = new List<Recommendation>();
            Retrofits = new List<Retrofit>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // AuditProjectReport
    public partial class AuditProjectReport
    {
        public string AuditProjectBsid { get; set; } // AuditProjectBSID
        public string UploadedBy { get; set; } // UploadedBy
        public DateTime UploadedDateTime { get; set; } // UploadedDateTime
        public string Url { get; set; } // Url
        public int Id { get; set; } // Id (Primary key)

        // Foreign keys
        public virtual AuditProject AuditProject { get; set; } // FK_AuditProjectReport_AuditProject

        public AuditProjectReport()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // AuditUploadBackup
    public partial class AuditUploadBackup
    {
        public long Id { get; set; } // Id (Primary key)
        public string UploadedBy { get; set; } // UploadedBy
        public DateTime UploadedDate { get; set; } // UploadedDate
        public string CompanyBsid { get; set; } // CompanyBSID
        public string ExternalId { get; set; } // ExternalId
        public string AuditDataXml { get; set; } // AuditDataXml
        public string ErrorMessage { get; set; } // ErrorMessage
        public string ErrorObject { get; set; } // ErrorObject
        public string AuditDataUrl { get; set; } // AuditDataUrl
        public bool IsDeletedAudit { get; set; } // IsDeletedAudit

        // Foreign keys
        public virtual Company Company { get; set; } // FK_AuditUploadBackup_Company

        public AuditUploadBackup()
        {
            UploadedDate = System.DateTime.Now;
            IsDeletedAudit = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // Building
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class Building
    {
        public string BuildingGuid { get; set; } // BuildingGUID (Primary key)
        public string AuditProjectBsid { get; set; } // AuditProjectBSID
        public string BuildingName { get; set; } // BuildingName
        public string BuildingCategory { get; set; } // BuildingCategory
        public string BuildingType { get; set; } // BuildingType
        public string Address1 { get; set; } // Address1
        public string Address2 { get; set; } // Address2
        public string Address3 { get; set; } // Address3
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Zip { get; set; } // Zip
        public string ZipExt { get; set; } // ZipExt
        public int? OccupantCount { get; set; } // OccupantCount
        public int? FloorsAbove { get; set; } // FloorsAbove
        public int? FloorsBelow { get; set; } // FloorsBelow
        public double? FloorAreaGross { get; set; } // FloorAreaGross
        public double? FloorAreaOccupied { get; set; } // FloorAreaOccupied
        public string BuildingHoursEquivalent { get; set; } // BuildingHoursEquivalent
        public string RateCode { get; set; } // RateCode
        public int? ZipZone { get; set; } // ZipZone
        public string BuildingBsid { get; set; } // BuildingBSID
        public string BuildingProjectId { get; set; } // BuildingProjectId
        public int? NumberOfUnits { get; set; } // NumberOfUnits
        public string UnitNumbering { get; set; } // UnitNumbering

        // Reverse navigation
        public virtual ICollection<BuildingAttachment> BuildingAttachments { get; set; } // Many to many mapping
        public virtual ICollection<BuildingElectricHistory> BuildingElectricHistories { get; set; } // Many to many mapping
        public virtual ICollection<BuildingEquipment> BuildingEquipments { get; set; } // BuildingEquipment.FK_BuildingEquipment_Building
        public virtual ICollection<BuildingGasHistory> BuildingGasHistories { get; set; } // Many to many mapping
        public virtual ICollection<BuildingSpace> BuildingSpaces { get; set; } // BuildingSpace.FK_BuildingSpace_Building
        public virtual ICollection<MultiFamily> MultiFamilies { get; set; } // MultiFamily.FK_MultiFamily_Building
        public virtual ICollection<Recommendation> Recommendations { get; set; } // Recommendation.FK_Recommendation_Building
        public virtual ICollection<Retrofit> Retrofits { get; set; } // Retrofit.FK_Retrofit_Building

        // Foreign keys
        public virtual AuditProject AuditProject { get; set; } // FK_Building_AuditProject

        public Building()
        {
            BuildingAttachments = new List<BuildingAttachment>();
            BuildingElectricHistories = new List<BuildingElectricHistory>();
            BuildingEquipments = new List<BuildingEquipment>();
            BuildingGasHistories = new List<BuildingGasHistory>();
            BuildingSpaces = new List<BuildingSpace>();
            MultiFamilies = new List<MultiFamily>();
            Recommendations = new List<Recommendation>();
            Retrofits = new List<Retrofit>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingAttachment
    public partial class BuildingAttachment
    {
        public string BuildingAttachmentGuid { get; set; } // BuildingAttachmentGUID (Primary key)
        public string BuildingGuid { get; set; } // BuildingGUID (Primary key)
        public string Code { get; set; } // Code
        public string Path { get; set; } // Path
        public string Name { get; set; } // Name

        // Foreign keys
        public virtual Building Building { get; set; } // FK_BuildingAttachment_Building

        public BuildingAttachment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingElectricHistory
    public partial class BuildingElectricHistory
    {
        public string BuildingGuid { get; set; } // BuildingGUID (Primary key)
        public DateTime ElectricHistoryReadDate { get; set; } // ElectricHistoryReadDate (Primary key)
        public int? BillDays { get; set; } // BillDays
        public int? CoolDays { get; set; } // CoolDays
        public int? HeatDays { get; set; } // HeatDays
        public double? TotalKwh { get; set; } // TotalKwh
        public double? OffPeakKwh { get; set; } // OffPeakKwh
        public double? OnPeakKwh { get; set; } // OnPeakKwh
        public double? BilledKw { get; set; } // BilledKw
        public double? MaximumCustomerKw { get; set; } // MaximumCustomerKw
        public double? TotalBill { get; set; } // TotalBill

        // Foreign keys
        public virtual Building Building { get; set; } // FK_BuildingElectricHistory_Building

        public BuildingElectricHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingEquipment
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class BuildingEquipment
    {
        public string BuildingEquipmentCid { get; set; } // BuildingEquipmentCID (Primary key)
        public string BuildingEquipmentMfid { get; set; } // BuildingEquipmentMFID
        public string BuildingGuid { get; set; } // BuildingGUID
        public string BallastManufacturerModel { get; set; } // BallastManufacturerModel
        public string BallastManufacturerName { get; set; } // BallastManufacturerName
        public string BurnerControlType { get; set; } // BurnerControlType
        public string BurnerManufacturerModel { get; set; } // BurnerManufacturerModel
        public string BurnerManufacturerName { get; set; } // BurnerManufacturerName
        public string BurnerType { get; set; } // BurnerType
        public double? Capacity { get; set; } // Capacity
        public string CapacityUnit { get; set; } // CapacityUnit
        public string CompressorType { get; set; } // CompressorType
        public string ControlSubType { get; set; } // ControlSubType
        public string EfficiencyDescription { get; set; } // EfficiencyDescription
        public double? EfficiencyAmt { get; set; } // EfficiencyAmt
        public string EquipmentDescription { get; set; } // EquipmentDescription
        public string EquipmentName { get; set; } // EquipmentName
        public string EquipmentTypeDescription { get; set; } // EquipmentTypeDescription
        public string LightHeatCoolingTypeDescription { get; set; } // LightHeatCoolingTypeDescription
        public string ManufacturerModel { get; set; } // ManufacturerModel
        public string ManufacturerName { get; set; } // ManufacturerName
        public double? PartLoadEfficiency { get; set; } // PartLoadEfficiency
        public string PartLoadEfficiencyUnit { get; set; } // PartLoadEfficiencyUnit
        public double? Quantity { get; set; } // Quantity
        public double? Size { get; set; } // Size
        public string SizeUnit { get; set; } // SizeUnit
        public string SupplementalControlType { get; set; } // SupplementalControlType
        public string SystemType { get; set; } // SystemType
        public string SystemTypeDescription { get; set; } // SystemTypeDescription
        public string SystemControlType { get; set; } // SystemControlType
        public string TypeOfEnergy { get; set; } // TypeOfEnergy
        public string WaterCoolingControlType { get; set; } // WaterCoolingControlType
        public string ActualWattage { get; set; } // ActualWattage
        public byte[] EquipmentImage { get; set; } // EquipmentImage
        public string ComponentId { get; set; } // ComponentId
        public string ComponentName { get; set; } // ComponentName
        public double? ControlFactor { get; set; } // ControlFactor
        public int? ControlQuantity { get; set; } // ControlQuantity
        public string EfficiencyUnit { get; set; } // EfficiencyUnit
        public string FilterString { get; set; } // FilterString
        public string PresetSchedule { get; set; } // PresetSchedule
        public bool? IsOccupencySensor { get; set; } // IsOccupencySensor
        public string RestoreUrl { get; set; } // RestoreUrl

        // Reverse navigation
        public virtual ICollection<BuildingEquipmentSchedule> BuildingEquipmentSchedules { get; set; } // BuildingEquipmentSchedule.FK_BuildingEquipmentSchedule_BuildingEquipment

        // Foreign keys
        public virtual Building Building { get; set; } // FK_BuildingEquipment_Building

        public BuildingEquipment()
        {
            BuildingEquipmentSchedules = new List<BuildingEquipmentSchedule>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingEquipmentSchedule
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class BuildingEquipmentSchedule
    {
        public string BuildingEquipmentScheduleCid { get; set; } // BuildingEquipmentScheduleCID (Primary key)
        public string BuildingEquipmentScheduleGuid { get; set; } // BuildingEquipmentScheduleGUID
        public string BuildingEquipmentCid { get; set; } // BuildingEquipmentCID
        public string ScheduleDescription { get; set; } // ScheduleDescription
        public string ScheduleId { get; set; } // ScheduleId
        public string ScheduleName { get; set; } // ScheduleName
        public string ScheduleType { get; set; } // ScheduleType
        public double? NumberDaysPerWeek { get; set; } // NumberDaysPerWeek
        public int? NumberHolidays { get; set; } // NumberHolidays
        public int? NumberWeeksPerYear { get; set; } // NumberWeeksPerYear

        // Reverse navigation
        public virtual ICollection<BuildingEquipmentScheduleDuration> BuildingEquipmentScheduleDurations { get; set; } // BuildingEquipmentScheduleDuration.FK_BuildingEquipmentScheduleDuration_BuildingEquipmentSchedule

        // Foreign keys
        public virtual BuildingEquipment BuildingEquipment { get; set; } // FK_BuildingEquipmentSchedule_BuildingEquipment

        public BuildingEquipmentSchedule()
        {
            BuildingEquipmentScheduleDurations = new List<BuildingEquipmentScheduleDuration>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingEquipmentScheduleDuration
    public partial class BuildingEquipmentScheduleDuration
    {
        public string BuildingEquipmentScheduleDurationGuid { get; set; } // BuildingEquipmentScheduleDurationGUID (Primary key)
        public string BuildingEquipmentScheduleCid { get; set; } // BuildingEquipmentScheduleCID
        public long? StartTicks { get; set; } // StartTicks
        public long? EndTicks { get; set; } // EndTicks
        public string DurationType { get; set; } // DurationType
        public TimeSpan? StartTime { get; set; } // StartTime
        public TimeSpan? EndTime { get; set; } // EndTime

        // Foreign keys
        public virtual BuildingEquipmentSchedule BuildingEquipmentSchedule { get; set; } // FK_BuildingEquipmentScheduleDuration_BuildingEquipmentSchedule

        public BuildingEquipmentScheduleDuration()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingGasHistory
    public partial class BuildingGasHistory
    {
        public string BuildingGuid { get; set; } // BuildingGUID (Primary key)
        public DateTime GasHistoryReadDate { get; set; } // GasHistoryReadDate (Primary key)
        public double? Therms { get; set; } // Therms
        public double? TotalBill { get; set; } // TotalBill
        public int? CoolDegreeDays { get; set; } // CoolDegreeDays
        public int? HeatDegreeDays { get; set; } // HeatDegreeDays
        public int? BillingDays { get; set; } // BillingDays

        // Foreign keys
        public virtual Building Building { get; set; } // FK_BuildingGasHistory_Building

        public BuildingGasHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingSpace
    public partial class BuildingSpace
    {
        public string BuildingSpaceCid { get; set; } // BuildingSpaceCID (Primary key)
        public string BuildingSpaceMfid { get; set; } // BuildingSpaceMFID
        public string BuildingGuid { get; set; } // BuildingGUID
        public string SpaceCode { get; set; } // SpaceCode
        public string SpaceLabel { get; set; } // SpaceLabel
        public string Note { get; set; } // Note

        // Foreign keys
        public virtual Building Building { get; set; } // FK_BuildingSpace_Building

        public BuildingSpace()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingType
    public partial class BuildingType
    {
        public string BuildingCategory { get; set; } // BuildingCategory (Primary key)
        public string BuildingType_ { get; set; } // BuildingType (Primary key)
        public int BuildingTypeId { get; set; } // BuildingTypeId

        public BuildingType()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // BuildingUnitType
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class BuildingUnitType
    {
        public string BuildingUnitTypeId { get; set; } // BuildingUnitTypeId (Primary key)
        public string MultiFamilyId { get; set; } // MultiFamilyId
        public string UnitType { get; set; } // UnitType
        public string UnitTypeName { get; set; } // UnitTypeName
        public int UnitTypeQuantity { get; set; } // UnitTypeQuantity
        public double SquareFeet { get; set; } // SquareFeet
        public int NumBedrooms { get; set; } // NumBedrooms
        public double NumBathrooms { get; set; } // NumBathrooms
        public string HeatingType { get; set; } // HeatingType
        public string CoolingType { get; set; } // CoolingType
        public string Location { get; set; } // Location
        public string DiOpportunity { get; set; } // DIOpportunity
        public string Inventory { get; set; } // Inventory
        public string AdditionalDetails { get; set; } // AdditionalDetails

        // Reverse navigation
        public virtual ICollection<RetrofitEstimate> RetrofitEstimates { get; set; } // RetrofitEstimate.FK_RetrofitEstimate_BuildingUnitType

        // Foreign keys
        public virtual MultiFamily MultiFamily { get; set; } // FK_BuildingUnitType_MultiFamily

        public BuildingUnitType()
        {
            RetrofitEstimates = new List<RetrofitEstimate>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // Company
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class Company
    {
        public string CompanyBsid { get; set; } // CompanyBSID (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string Address1 { get; set; } // Address1
        public string Address2 { get; set; } // Address2
        public string Address3 { get; set; } // Address3
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Zip { get; set; } // Zip
        public string ZipExt { get; set; } // ZipExt
        public string ElectricAccountNumber { get; set; } // ElectricAccountNumber
        public string ElectricRateCode { get; set; } // ElectricRateCode
        public string GasAccountNumber { get; set; } // GasAccountNumber
        public string GasRateCode { get; set; } // GasRateCode
        public string RecordType { get; set; } // RecordType
        public string EmailAddress { get; set; } // EmailAddress
        public string PhoneNumber { get; set; } // PhoneNumber

        // Reverse navigation
        public virtual ICollection<AuditProject> AuditProjects { get; set; } // AuditProject.FK_AuditProject_Company
        public virtual ICollection<AuditUploadBackup> AuditUploadBackups { get; set; } // AuditUploadBackup.FK_AuditUploadBackup_Company
        public virtual ICollection<Contact> Contacts { get; set; } // Contact.FK_Contact_Company
        public virtual ICollection<EmailRequestLog> EmailRequestLogs { get; set; } // EmailRequestLog.FK_EmailRequestLog_Company

        public Company()
        {
            AuditProjects = new List<AuditProject>();
            AuditUploadBackups = new List<AuditUploadBackup>();
            Contacts = new List<Contact>();
            EmailRequestLogs = new List<EmailRequestLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // Contact
    public partial class Contact
    {
        public string ContactGuid { get; set; } // ContactGUID (Primary key)
        public string CompanyBsid { get; set; } // CompanyBSID
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public string Note { get; set; } // Note
        public string JobRole { get; set; } // JobRole
        public string EmailAddress { get; set; } // EmailAddress
        public string PhoneNumber { get; set; } // PhoneNumber

        // Foreign keys
        public virtual Company Company { get; set; } // FK_Contact_Company

        public Contact()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIAccount
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class DiAccount
    {
        public string DiAccountBsid { get; set; } // DIAccountBSID (Primary key)
        public string Name { get; set; } // Name
        public string Address1 { get; set; } // Address1
        public string Address2 { get; set; } // Address2
        public string Address3 { get; set; } // Address3
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Zip { get; set; } // Zip
        public string ZipExt { get; set; } // ZipExt
        public string ElectricAccountNumber { get; set; } // ElectricAccountNumber
        public string ElectricRateCode { get; set; } // ElectricRateCode
        public string GasAccountNumber { get; set; } // GasAccountNumber
        public string GasRateCode { get; set; } // GasRateCode
        public string RecordType { get; set; } // RecordType
        public string EmailAddress { get; set; } // EmailAddress
        public string PhoneNumber { get; set; } // PhoneNumber

        // Reverse navigation
        public virtual ICollection<DiContact> DiContacts { get; set; } // DIContact.FK_DIContact_DIAccount
        public virtual ICollection<DiElectricHistory> DiElectricHistories { get; set; } // Many to many mapping
        public virtual ICollection<DiGasHistory> DiGasHistories { get; set; } // Many to many mapping
        public virtual ICollection<DiProjectInfo> DiProjectInfoes { get; set; } // DIProjectInfo.FK_DIProjectInfo_DIAccount
        public virtual ICollection<DiUploadBackup> DiUploadBackups { get; set; } // DIUploadBackup.FK_DIUploadBackup_DIAccount

        public DiAccount()
        {
            DiContacts = new List<DiContact>();
            DiElectricHistories = new List<DiElectricHistory>();
            DiGasHistories = new List<DiGasHistory>();
            DiProjectInfoes = new List<DiProjectInfo>();
            DiUploadBackups = new List<DiUploadBackup>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIAnswer
    public partial class DiAnswer
    {
        public string DiQuestionId { get; set; } // DIQuestionID (Primary key)
        public string DiProjectInfoId { get; set; } // DIProjectInfoID (Primary key)
        public DateTime AnsweredOn { get; set; } // AnsweredOn
        public string Value { get; set; } // Value

        // Foreign keys
        public virtual DiProjectInfo DiProjectInfo { get; set; } // FK_DIAnswer_DIProjectInfo

        public DiAnswer()
        {
            AnsweredOn = System.DateTime.Now;
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIAttachment
    public partial class DiAttachment
    {
        public string DiAttachmentId { get; set; } // DIAttachmentID (Primary key)
        public string DiProjectInfoId { get; set; } // DIProjectInfoID
        public string RestoreUrl { get; set; } // RestoreUrl
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public string Comments { get; set; } // Comments
        public string AttachmentFile { get; set; } // AttachmentFile
        public string Type { get; set; } // Type
        public string Category { get; set; } // Category
        public bool? IncludeInReport { get; set; } // IncludeInReport
        public bool? IncludeOnCover { get; set; } // IncludeOnCover

        // Foreign keys
        public virtual DiProjectInfo DiProjectInfo { get; set; } // FK_DIAttachment_DIProjectInfo

        public DiAttachment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIContact
    public partial class DiContact
    {
        public string DiContactGuid { get; set; } // DIContactGUID (Primary key)
        public string DiAccountBsid { get; set; } // DIAccountBSID
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public string Note { get; set; } // Note
        public string JobRole { get; set; } // JobRole
        public string EmailAddress { get; set; } // EmailAddress
        public string PhoneNumber { get; set; } // PhoneNumber

        // Foreign keys
        public virtual DiAccount DiAccount { get; set; } // FK_DIContact_DIAccount

        public DiContact()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIElectricHistory
    public partial class DiElectricHistory
    {
        public string DiAccountBsid { get; set; } // DIAccountBSID (Primary key)
        public DateTime ElectricHistoryReadDate { get; set; } // ElectricHistoryReadDate (Primary key)
        public int? BillDays { get; set; } // BillDays
        public int? CoolDays { get; set; } // CoolDays
        public int? HeatDays { get; set; } // HeatDays
        public double? TotalKwh { get; set; } // TotalKwh
        public double? OffPeakKwh { get; set; } // OffPeakKwh
        public double? OnPeakKwh { get; set; } // OnPeakKwh
        public double? BilledKw { get; set; } // BilledKw
        public double? MaximumCustomerKw { get; set; } // MaximumCustomerKw
        public double? TotalBill { get; set; } // TotalBill

        // Foreign keys
        public virtual DiAccount DiAccount { get; set; } // FK_DIElectricHistory_Building

        public DiElectricHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIGasHistory
    public partial class DiGasHistory
    {
        public string DiAccountBsid { get; set; } // DIAccountBSID (Primary key)
        public DateTime GasHistoryReadDate { get; set; } // GasHistoryReadDate (Primary key)
        public double? Therms { get; set; } // Therms
        public double? TotalBill { get; set; } // TotalBill
        public int? CoolDegreeDays { get; set; } // CoolDegreeDays
        public int? HeatDegreeDays { get; set; } // HeatDegreeDays
        public int? BillingDays { get; set; } // BillingDays

        // Foreign keys
        public virtual DiAccount DiAccount { get; set; } // FK_DIGasHistory_Building

        public DiGasHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIInkSecureSignatureData
    public partial class DiInkSecureSignatureData
    {
        public string DiProjectInfoId { get; set; } // DIProjectInfoID (Primary key)
        public DateTime AcquiredSignatureStartOn { get; set; } // AcquiredSignatureStartOn
        public DateTime BiometricEncryptionCompletedOn { get; set; } // BiometricEncryptionCompletedOn
        public DateTime BiometricEncryptionSubmittedOn { get; set; } // BiometricEncryptionSubmittedOn
        public string EncryptedBiometricData { get; set; } // EncryptedBiometricData
        public string HardwareInfo { get; set; } // HardwareInfo
        public string InkWashedSignature { get; set; } // InkWashedSignature
        public DateTime SignerAcceptedOn { get; set; } // SignerAcceptedOn
        public string SignersName { get; set; } // SignersName

        // Foreign keys
        public virtual DiProjectInfo DiProjectInfo { get; set; } // FK_DIInkSecureSignatureData_AuditProject

        public DiInkSecureSignatureData()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIProjectInfo
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class DiProjectInfo
    {
        public string DiProjectInfoId { get; set; } // DIProjectInfoID (Primary key)
        public string DiAccountBsid { get; set; } // DIAccountBSID
        public string ExternalId { get; set; } // ExternalId
        public string ProgramId { get; set; } // ProgramID
        public DateTime? ProjectCompleteDate { get; set; } // ProjectCompleteDate
        public string ProjectName { get; set; } // ProjectName
        public string EnergyAdvisorName { get; set; } // EnergyAdvisorName
        public DateTime? ActualStartTime { get; set; } // ActualStartTime
        public DateTime? ScheduledStartTime { get; set; } // ScheduledStartTime
        public bool IsReportEmailSent { get; set; } // IsReportEmailSent

        // Reverse navigation
        public virtual DiInkSecureSignatureData DiInkSecureSignatureData { get; set; } // DIInkSecureSignatureData.FK_DIInkSecureSignatureData_AuditProject
        public virtual ICollection<DiAnswer> DiAnswers { get; set; } // Many to many mapping
        public virtual ICollection<DiAttachment> DiAttachments { get; set; } // DIAttachment.FK_DIAttachment_DIProjectInfo
        public virtual ICollection<DiRetrofit> DiRetrofits { get; set; } // DIRetrofit.FK_DIRetrofit_DIProjectInfo
        public virtual ICollection<DiUploadBackup> DiUploadBackups { get; set; } // DIUploadBackup.FK_DIUploadBackup_DIProject

        // Foreign keys
        public virtual DiAccount DiAccount { get; set; } // FK_DIProjectInfo_DIAccount

        public DiProjectInfo()
        {
            //IsReportEmailSent = false;
            DiAnswers = new List<DiAnswer>();
            DiAttachments = new List<DiAttachment>();
            DiRetrofits = new List<DiRetrofit>();
            DiUploadBackups = new List<DiUploadBackup>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIRetrofit
    public partial class DiRetrofit
    {
        public string DiRetrofitGuid { get; set; } // DIRetrofitGUID (Primary key)
        public string DiProjectInfoId { get; set; } // DIProjectInfoID
        public string ComponentBsid { get; set; } // ComponentBSID
        public string ProgramId { get; set; } // ProgramID
        public string Description { get; set; } // Description
        public double Quantity { get; set; } // Quantity
        public double? Kwh { get; set; } // Kwh
        public double? Therms { get; set; } // Therms
        public double? Water { get; set; } // Water
        public decimal? Savings { get; set; } // Savings
        public string Space { get; set; } // Space
        public double? Incentive { get; set; } // Incentive
        public string IconPath { get; set; } // IconPath
        public double? KWhUnit { get; set; } // KWhUnit
        public double? ThermsUnit { get; set; } // ThermsUnit
        public string IconFileName { get; set; } // IconFileName
        public string LineItem1 { get; set; } // LineItem1
        public string LineItem2 { get; set; } // LineItem2
        public string LineItem3 { get; set; } // LineItem3
        public string LineItem4 { get; set; } // LineItem4

        // Foreign keys
        public virtual DiProjectInfo DiProjectInfo { get; set; } // FK_DIRetrofit_DIProjectInfo

        public DiRetrofit()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // DIUploadBackup
    public partial class DiUploadBackup
    {
        public long Id { get; set; } // Id (Primary key)
        public string UploadedBy { get; set; } // UploadedBy
        public DateTime UploadedDate { get; set; } // UploadedDate
        public string DiAccountBsid { get; set; } // DIAccountBSID
        public string DiProjectInfoId { get; set; } // DIProjectInfoID
        public string DiId { get; set; } // DIId
        public string DiDataXml { get; set; } // DIDataXml
        public string ErrorMessage { get; set; } // ErrorMessage
        public string ErrorObject { get; set; } // ErrorObject
        public string DiDataUrl { get; set; } // DIDataUrl
        public bool IsDeletedDi { get; set; } // IsDeletedDi
        public string ExternalId { get; set; } // ExternalId

        // Foreign keys
        public virtual DiAccount DiAccount { get; set; } // FK_DIUploadBackup_DIAccount
        public virtual DiProjectInfo DiProjectInfo { get; set; } // FK_DIUploadBackup_DIProject

        public DiUploadBackup()
        {
            UploadedDate = System.DateTime.Now;
            IsDeletedDi = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // EmailRequestLog
    public partial class EmailRequestLog
    {
        public int Id { get; set; } // Id (Primary key)
        public DateTime DateTime { get; set; } // DateTime
        public string CompanyBsid { get; set; } // CompanyBSID
        public bool? EmailSent { get; set; } // EmailSent
        public string ToAddress { get; set; } // ToAddress
        public string RequestedBy { get; set; } // RequestedBy

        // Foreign keys
        public virtual Company Company { get; set; } // FK_EmailRequestLog_Company

        public EmailRequestLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // InkSecureSignatureData
    public partial class InkSecureSignatureData
    {
        public string AuditProjectBsid { get; set; } // AuditProjectBSID (Primary key)
        public DateTime AcquiredSignatureStartOn { get; set; } // AcquiredSignatureStartOn
        public DateTime BiometricEncryptionCompletedOn { get; set; } // BiometricEncryptionCompletedOn
        public DateTime BiometricEncryptionSubmittedOn { get; set; } // BiometricEncryptionSubmittedOn
        public string EncryptedBiometricData { get; set; } // EncryptedBiometricData
        public string HardwareInfo { get; set; } // HardwareInfo
        public string InkWashedSignature { get; set; } // InkWashedSignature
        public DateTime SignerAcceptedOn { get; set; } // SignerAcceptedOn
        public string SignersName { get; set; } // SignersName

        // Foreign keys
        public virtual AuditProject AuditProject { get; set; } // FK_InkSecureSignatureData_AuditProject

        public InkSecureSignatureData()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // MultiFamily
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class MultiFamily
    {
        public string MultiFamilyId { get; set; } // MultiFamilyId (Primary key)
        public string BuildingGuid { get; set; } // BuildingGUID

        // Reverse navigation
        public virtual ICollection<BuildingUnitType> BuildingUnitTypes { get; set; } // BuildingUnitType.FK_BuildingUnitType_MultiFamily

        // Foreign keys
        public virtual Building Building { get; set; } // FK_MultiFamily_Building

        public MultiFamily()
        {
            BuildingUnitTypes = new List<BuildingUnitType>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // Recommendation
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class Recommendation
    {
        public string RecommendationGuid { get; set; } // RecommendationGUID
        public string BuildingGuid { get; set; } // BuildingGUID
        public string AuditProjectBsid { get; set; } // AuditProjectBSID
        public string RecommendationName { get; set; } // RecommendationName
        public string RecommendationDescription { get; set; } // RecommendationDescription
        public bool? IncludedInReport { get; set; } // IncludedInReport
        public int? ReportRank { get; set; } // ReportRank
        public bool? IsOccupancySensor { get; set; } // IsOccupancySensor
        public string InstanceId { get; set; } // InstanceId (Primary key)

        // Reverse navigation
        public virtual ICollection<RecommendationOption> RecommendationOptions { get; set; } // RecommendationOption.FK_RecommendationOption_Recommendation

        // Foreign keys
        public virtual AuditProject AuditProject { get; set; } // FK_Recommendation_AuditProject
        public virtual Building Building { get; set; } // FK_Recommendation_Building

        public Recommendation()
        {
            RecommendationOptions = new List<RecommendationOption>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // RecommendationOption
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public partial class RecommendationOption
    {
        public string RecommendationOptionGuid { get; set; } // RecommendationOptionGUID (Primary key)
        public string RecommendationGuid { get; set; } // RecommendationGUID
        public string RecommendationName { get; set; } // RecommendationName
        public string OptionDescription { get; set; } // OptionDescription
        public string OptionId { get; set; } // OptionId
        public string OptionName { get; set; } // OptionName
        public double? Cop { get; set; } // Cop
        public string ElectricDisplayAs { get; set; } // ElectricDisplayAs
        public double? EnergyFactor { get; set; } // EnergyFactor
        public string EnergySource { get; set; } // EnergySource
        public string GasDisplayAs { get; set; } // GasDisplayAs
        public double? HeatingCoolingHours { get; set; } // HeatingCoolingHours
        public bool? IsOccupancySensor { get; set; } // IsOccupancySensor
        public bool? IsZeroSavings { get; set; } // IsZeroSavings
        public double? KwhSaved { get; set; } // KwhSaved
        public double? KwhSavedWithRateCode { get; set; } // KwhSavedWithRateCode
        public string OccupancySensorDisplayAs { get; set; } // OccupancySensorDisplayAs
        public string OriginalEquipmentMfid { get; set; } // OriginalEquipmentMFID
        public int? Quantity { get; set; } // Quantity
        public string RebateBsid { get; set; } // RebateBSID
        public string RebateCalculationEquation { get; set; } // RebateCalculationEquation
        public string RebateClientRefId { get; set; } // RebateClientRefID
        public double? RebateValue { get; set; } // RebateValue
        public double? Savings { get; set; } // Savings
        public string SavingsCalculationEquationSaving { get; set; } // SavingsCalculationEquationSaving
        public double? ThermsSaved { get; set; } // ThermsSaved
        public double? ThermsSavedWithRateCode { get; set; } // ThermsSavedWithRateCode
        public string TypeOfEnergy { get; set; } // TypeOfEnergy

        // Reverse navigation
        public virtual ICollection<RecommendationOptionEquipment> RecommendationOptionEquipments { get; set; } // RecommendationOptionEquipment.FK_RecommendationOptionEquipment_RecommendationOption

        // Foreign keys
        public virtual Recommendation Recommendation { get; set; } // FK_RecommendationOption_Recommendation

        public RecommendationOption()
        {
            RecommendationOptionEquipments = new List<RecommendationOptionEquipment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // RecommendationOptionEquipment
    public partial class RecommendationOptionEquipment
    {
        public string RecommendationOptionEquipmentCid { get; set; } // RecommendationOptionEquipmentCID (Primary key)
        public string RecommendationOptionEquipmentMfid { get; set; } // RecommendationOptionEquipmentMFID
        public string RecommendationOptionGuid { get; set; } // RecommendationOptionGUID
        public string ActualWattage { get; set; } // ActualWattage
        public double? AnnualHours { get; set; } // AnnualHours
        public double? Efficiency { get; set; } // Efficiency
        public string EfficiencyUnit { get; set; } // EfficiencyUnit
        public string EfficiencyDescription { get; set; } // EfficiencyDescription
        public string EquipmentDescription { get; set; } // EquipmentDescription
        public string EquipmentName { get; set; } // EquipmentName
        public double? Quantity { get; set; } // Quantity
        public string RecommendationEquipmentId { get; set; } // RecommendationEquipmentId
        public double? Size { get; set; } // Size
        public string SizeUnit { get; set; } // SizeUnit
        public string SystemType { get; set; } // SystemType
        public double? ThermalEfficiency { get; set; } // ThermalEfficiency

        // Foreign keys
        public virtual RecommendationOption RecommendationOption { get; set; } // FK_RecommendationOptionEquipment_RecommendationOption

        public RecommendationOptionEquipment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // ReportTracking
    public partial class ReportTracking
    {
        public int Id { get; set; } // Id (Primary key)
        public string Type { get; set; } // Type
        public string AccountId { get; set; } // AccountId
        public string ProjectId { get; set; } // ProjectId
        public string UploadedBy { get; set; } // UploadedBy
        public DateTime UploadedDateTime { get; set; } // UploadedDateTime
        public string Url { get; set; } // Url

        public ReportTracking()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // Retrofit
    public partial class Retrofit
    {
        public string RetrofitGuid { get; set; } // RetrofitGUID (Primary key)
        public string BuildingGuid { get; set; } // BuildingGUID
        public string AuditProjectBsid { get; set; } // AuditProjectBSID
        public string ComponentBsid { get; set; } // ComponentBSID
        public string ProgramId { get; set; } // ProgramID
        public string Description { get; set; } // Description
        public int Quantity { get; set; } // Quantity
        public double? Kwh { get; set; } // Kwh
        public double? Therms { get; set; } // Therms
        public double? Water { get; set; } // Water
        public decimal? Savings { get; set; } // Savings
        public string Space { get; set; } // Space
        public double? Incentive { get; set; } // Incentive
        public string IconPath { get; set; } // IconPath

        // Foreign keys
        public virtual AuditProject AuditProject { get; set; } // FK_Retrofit_AuditProject
        public virtual Building Building { get; set; } // FK_Retrofit_Building

        public Retrofit()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // RetrofitEstimate
    public partial class RetrofitEstimate
    {
        public string RetrofitGuid { get; set; } // RetrofitGUID (Primary key)
        public string BuildingUnitTypeId { get; set; } // BuildingUnitTypeId
        public string ComponentBsid { get; set; } // ComponentBSID
        public string ProgramId { get; set; } // ProgramID
        public string Description { get; set; } // Description
        public int Quantity { get; set; } // Quantity
        public double? Kwh { get; set; } // Kwh
        public double? Therms { get; set; } // Therms
        public double? Water { get; set; } // Water
        public decimal? Savings { get; set; } // Savings
        public string Space { get; set; } // Space
        public double? Incentive { get; set; } // Incentive
        public string IconPath { get; set; } // IconPath

        // Foreign keys
        public virtual BuildingUnitType BuildingUnitType { get; set; } // FK_RetrofitEstimate_BuildingUnitType

        public RetrofitEstimate()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // database_firewall_rules
    public partial class sys_DatabaseFirewallRule
    {
        public int Id { get; set; } // id
        public string Name { get; set; } // name
        public string StartIpAddress { get; set; } // start_ip_address
        public string EndIpAddress { get; set; } // end_ip_address
        public DateTime CreateDate { get; set; } // create_date
        public DateTime ModifyDate { get; set; } // modify_date

        public sys_DatabaseFirewallRule()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // script_deployments
    public partial class sys_ScriptDeployment
    {
        public Guid DeploymentId { get; set; } // deployment_id
        public Guid CoordinatorId { get; set; } // coordinator_id
        public string DeploymentName { get; set; } // deployment_name
        public DateTimeOffset DeploymentSubmitted { get; set; } // deployment_submitted
        public DateTimeOffset? DeploymentStart { get; set; } // deployment_start
        public DateTimeOffset? DeploymentEnd { get; set; } // deployment_end
        public string Status { get; set; } // status
        public string ResultsTable { get; set; } // results_table
        public string RetryPolicy { get; set; } // retry_policy
        public string Script { get; set; } // script
        public string Messages { get; set; } // messages

        public sys_ScriptDeployment()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // script_deployment_status
    public partial class sys_ScriptDeploymentStatus
    {
        public Guid DeploymentId { get; set; } // deployment_id
        public Guid? WorkerId { get; set; } // worker_id
        public string LogicalServer { get; set; } // logical_server
        public string DatabaseName { get; set; } // database_name
        public DateTimeOffset? DeploymentStart { get; set; } // deployment_start
        public DateTimeOffset? DeploymentEnd { get; set; } // deployment_end
        public string Status { get; set; } // status
        public short NumRetries { get; set; } // num_retries
        public string Messages { get; set; } // messages

        public sys_ScriptDeploymentStatus()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    // UploadedFile
    public partial class UploadedFile
    {
        public int Id { get; set; } // Id (Primary key)
        public string Type { get; set; } // Type
        public string AccountId { get; set; } // AccountId
        public string ProjectId { get; set; } // ProjectId
        public string UploadedBy { get; set; } // UploadedBy
        public DateTime UploadedDateTime { get; set; } // UploadedDateTime
        public string Url { get; set; } // Url
        public string UploadedFileType { get; set; } // UploadedFileType

        public UploadedFile()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }


    // ************************************************************************
    // POCO Configuration

    // AuditProject
    internal partial class AuditProjectMap : EntityTypeConfiguration<AuditProject>
    {
        public AuditProjectMap()
            : this("dbo")
        {
        }

        public AuditProjectMap(string schema)
        {
            ToTable(schema + ".AuditProject");
            HasKey(x => x.AuditProjectBsid);

            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyBsid).HasColumnName("CompanyBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProgramId).HasColumnName("ProgramID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(10);
            Property(x => x.AuditProjectName).HasColumnName("AuditProjectName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.AuditProjectDescription).HasColumnName("AuditProjectDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.ScheduledStart).HasColumnName("ScheduledStart").IsOptional().HasColumnType("datetime");
            Property(x => x.ScheduledEnd).HasColumnName("ScheduledEnd").IsOptional().HasColumnType("datetime");
            Property(x => x.CompanyContact).HasColumnName("CompanyContact").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.AuditStatus).HasColumnName("AuditStatus").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.AuditCompleteDate).HasColumnName("AuditCompleteDate").IsOptional().HasColumnType("datetime");
            Property(x => x.EnergyAdvisorName).HasColumnName("EnergyAdvisorName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ClientAccountId).HasColumnName("ClientAccountId").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ElectricDisplayAs).HasColumnName("ElectricDisplayAs").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.GasDisplayAs).HasColumnName("GasDisplayAs").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.IsAdHocAudit).HasColumnName("IsAdHocAudit").IsRequired().HasColumnType("bit");
            Property(x => x.IsReportEmailSent).HasColumnName("IsReportEmailSent").IsRequired().HasColumnType("bit");

            // Foreign keys
            HasRequired(a => a.Company).WithMany(b => b.AuditProjects).HasForeignKey(c => c.CompanyBsid); // FK_AuditProject_Company
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // AuditProjectReport
    internal partial class AuditProjectReportMap : EntityTypeConfiguration<AuditProjectReport>
    {
        public AuditProjectReportMap()
            : this("dbo")
        {
        }

        public AuditProjectReportMap(string schema)
        {
            ToTable(schema + ".AuditProjectReport");
            HasKey(x => x.Id);

            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.UploadedBy).HasColumnName("UploadedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.UploadedDateTime).HasColumnName("UploadedDateTime").IsRequired().HasColumnType("datetime2");
            Property(x => x.Url).HasColumnName("Url").IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Foreign keys
            HasRequired(a => a.AuditProject).WithMany(b => b.AuditProjectReports).HasForeignKey(c => c.AuditProjectBsid); // FK_AuditProjectReport_AuditProject
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // AuditUploadBackup
    internal partial class AuditUploadBackupMap : EntityTypeConfiguration<AuditUploadBackup>
    {
        public AuditUploadBackupMap()
            : this("dbo")
        {
        }

        public AuditUploadBackupMap(string schema)
        {
            ToTable(schema + ".AuditUploadBackup");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("bigint").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UploadedBy).HasColumnName("UploadedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.UploadedDate).HasColumnName("UploadedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.CompanyBsid).HasColumnName("CompanyBSID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ExternalId).HasColumnName("ExternalId").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.AuditDataXml).HasColumnName("AuditDataXml").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ErrorMessage).HasColumnName("ErrorMessage").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ErrorObject).HasColumnName("ErrorObject").IsOptional().HasColumnType("nvarchar");
            Property(x => x.AuditDataUrl).HasColumnName("AuditDataUrl").IsOptional().HasColumnType("nvarchar").HasMaxLength(300);
            Property(x => x.IsDeletedAudit).HasColumnName("IsDeletedAudit").IsRequired().HasColumnType("bit");

            // Foreign keys
            HasOptional(a => a.Company).WithMany(b => b.AuditUploadBackups).HasForeignKey(c => c.CompanyBsid); // FK_AuditUploadBackup_Company
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Building
    internal partial class BuildingMap : EntityTypeConfiguration<Building>
    {
        public BuildingMap()
            : this("dbo")
        {
        }

        public BuildingMap(string schema)
        {
            ToTable(schema + ".Building");
            HasKey(x => x.BuildingGuid);

            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingName).HasColumnName("BuildingName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.BuildingCategory).HasColumnName("BuildingCategory").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.BuildingType).HasColumnName("BuildingType").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Address1).HasColumnName("Address1").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address2).HasColumnName("Address2").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address3).HasColumnName("Address3").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.State).HasColumnName("State").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.Zip).HasColumnName("Zip").IsOptional().HasColumnType("nvarchar").HasMaxLength(6);
            Property(x => x.ZipExt).HasColumnName("ZipExt").IsOptional().HasColumnType("nvarchar").HasMaxLength(4);
            Property(x => x.OccupantCount).HasColumnName("OccupantCount").IsOptional().HasColumnType("int");
            Property(x => x.FloorsAbove).HasColumnName("FloorsAbove").IsOptional().HasColumnType("int");
            Property(x => x.FloorsBelow).HasColumnName("FloorsBelow").IsOptional().HasColumnType("int");
            Property(x => x.FloorAreaGross).HasColumnName("FloorAreaGross").IsOptional().HasColumnType("float");
            Property(x => x.FloorAreaOccupied).HasColumnName("FloorAreaOccupied").IsOptional().HasColumnType("float");
            Property(x => x.BuildingHoursEquivalent).HasColumnName("BuildingHoursEquivalent").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.RateCode).HasColumnName("RateCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ZipZone).HasColumnName("ZipZone").IsOptional().HasColumnType("int");
            Property(x => x.BuildingBsid).HasColumnName("BuildingBSID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingProjectId).HasColumnName("BuildingProjectId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.NumberOfUnits).HasColumnName("NumberOfUnits").IsOptional().HasColumnType("int");
            Property(x => x.UnitNumbering).HasColumnName("UnitNumbering").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.AuditProject).WithMany(b => b.Buildings).HasForeignKey(c => c.AuditProjectBsid); // FK_Building_AuditProject
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingAttachment
    internal partial class BuildingAttachmentMap : EntityTypeConfiguration<BuildingAttachment>
    {
        public BuildingAttachmentMap()
            : this("dbo")
        {
        }

        public BuildingAttachmentMap(string schema)
        {
            ToTable(schema + ".BuildingAttachment");
            HasKey(x => new { x.BuildingAttachmentGuid, x.BuildingGuid });

            Property(x => x.BuildingAttachmentGuid).HasColumnName("BuildingAttachmentGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Code).HasColumnName("Code").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Path).HasColumnName("Path").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.BuildingAttachments).HasForeignKey(c => c.BuildingGuid); // FK_BuildingAttachment_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingElectricHistory
    internal partial class BuildingElectricHistoryMap : EntityTypeConfiguration<BuildingElectricHistory>
    {
        public BuildingElectricHistoryMap()
            : this("dbo")
        {
        }

        public BuildingElectricHistoryMap(string schema)
        {
            ToTable(schema + ".BuildingElectricHistory");
            HasKey(x => new { x.BuildingGuid, x.ElectricHistoryReadDate });

            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ElectricHistoryReadDate).HasColumnName("ElectricHistoryReadDate").IsRequired().HasColumnType("date").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BillDays).HasColumnName("BillDays").IsOptional().HasColumnType("int");
            Property(x => x.CoolDays).HasColumnName("CoolDays").IsOptional().HasColumnType("int");
            Property(x => x.HeatDays).HasColumnName("HeatDays").IsOptional().HasColumnType("int");
            Property(x => x.TotalKwh).HasColumnName("TotalKwh").IsOptional().HasColumnType("float");
            Property(x => x.OffPeakKwh).HasColumnName("OffPeakKwh").IsOptional().HasColumnType("float");
            Property(x => x.OnPeakKwh).HasColumnName("OnPeakKwh").IsOptional().HasColumnType("float");
            Property(x => x.BilledKw).HasColumnName("BilledKw").IsOptional().HasColumnType("float");
            Property(x => x.MaximumCustomerKw).HasColumnName("MaximumCustomerKw").IsOptional().HasColumnType("float");
            Property(x => x.TotalBill).HasColumnName("TotalBill").IsOptional().HasColumnType("float");

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.BuildingElectricHistories).HasForeignKey(c => c.BuildingGuid); // FK_BuildingElectricHistory_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingEquipment
    internal partial class BuildingEquipmentMap : EntityTypeConfiguration<BuildingEquipment>
    {
        public BuildingEquipmentMap()
            : this("dbo")
        {
        }

        public BuildingEquipmentMap(string schema)
        {
            ToTable(schema + ".BuildingEquipment");
            HasKey(x => x.BuildingEquipmentCid);

            Property(x => x.BuildingEquipmentCid).HasColumnName("BuildingEquipmentCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingEquipmentMfid).HasColumnName("BuildingEquipmentMFID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BallastManufacturerModel).HasColumnName("BallastManufacturerModel").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BallastManufacturerName).HasColumnName("BallastManufacturerName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BurnerControlType).HasColumnName("BurnerControlType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BurnerManufacturerModel).HasColumnName("BurnerManufacturerModel").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BurnerManufacturerName).HasColumnName("BurnerManufacturerName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BurnerType).HasColumnName("BurnerType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Capacity).HasColumnName("Capacity").IsOptional().HasColumnType("float");
            Property(x => x.CapacityUnit).HasColumnName("CapacityUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.CompressorType).HasColumnName("CompressorType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ControlSubType).HasColumnName("ControlSubType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.EfficiencyDescription).HasColumnName("EfficiencyDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.EfficiencyAmt).HasColumnName("EfficiencyAmt").IsOptional().HasColumnType("float");
            Property(x => x.EquipmentDescription).HasColumnName("EquipmentDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.EquipmentName).HasColumnName("EquipmentName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.EquipmentTypeDescription).HasColumnName("EquipmentTypeDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.LightHeatCoolingTypeDescription).HasColumnName("LightHeatCoolingTypeDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ManufacturerModel).HasColumnName("ManufacturerModel").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ManufacturerName).HasColumnName("ManufacturerName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.PartLoadEfficiency).HasColumnName("PartLoadEfficiency").IsOptional().HasColumnType("float");
            Property(x => x.PartLoadEfficiencyUnit).HasColumnName("PartLoadEfficiencyUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("Quantity").IsOptional().HasColumnType("float");
            Property(x => x.Size).HasColumnName("Size").IsOptional().HasColumnType("float");
            Property(x => x.SizeUnit).HasColumnName("SizeUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.SupplementalControlType).HasColumnName("SupplementalControlType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.SystemType).HasColumnName("SystemType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.SystemTypeDescription).HasColumnName("SystemTypeDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.SystemControlType).HasColumnName("SystemControlType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.TypeOfEnergy).HasColumnName("TypeOfEnergy").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.WaterCoolingControlType).HasColumnName("WaterCoolingControlType").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ActualWattage).HasColumnName("ActualWattage").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.EquipmentImage).HasColumnName("EquipmentImage").IsOptional().HasColumnType("image").HasMaxLength(2147483647);
            Property(x => x.ComponentId).HasColumnName("ComponentId").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ComponentName).HasColumnName("ComponentName").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ControlFactor).HasColumnName("ControlFactor").IsOptional().HasColumnType("float");
            Property(x => x.ControlQuantity).HasColumnName("ControlQuantity").IsOptional().HasColumnType("int");
            Property(x => x.EfficiencyUnit).HasColumnName("EfficiencyUnit").IsOptional().HasColumnType("nvarchar");
            Property(x => x.FilterString).HasColumnName("FilterString").IsOptional().HasColumnType("nvarchar");
            Property(x => x.PresetSchedule).HasColumnName("PresetSchedule").IsOptional().HasColumnType("nvarchar");
            Property(x => x.IsOccupencySensor).HasColumnName("IsOccupencySensor").IsOptional().HasColumnType("bit");
            Property(x => x.RestoreUrl).HasColumnName("RestoreUrl").IsOptional().HasColumnType("nvarchar").HasMaxLength(400);

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.BuildingEquipments).HasForeignKey(c => c.BuildingGuid); // FK_BuildingEquipment_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingEquipmentSchedule
    internal partial class BuildingEquipmentScheduleMap : EntityTypeConfiguration<BuildingEquipmentSchedule>
    {
        public BuildingEquipmentScheduleMap()
            : this("dbo")
        {
        }

        public BuildingEquipmentScheduleMap(string schema)
        {
            ToTable(schema + ".BuildingEquipmentSchedule");
            HasKey(x => x.BuildingEquipmentScheduleCid);

            Property(x => x.BuildingEquipmentScheduleCid).HasColumnName("BuildingEquipmentScheduleCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingEquipmentScheduleGuid).HasColumnName("BuildingEquipmentScheduleGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingEquipmentCid).HasColumnName("BuildingEquipmentCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ScheduleDescription).HasColumnName("ScheduleDescription").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(1000);
            Property(x => x.ScheduleId).HasColumnName("ScheduleId").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ScheduleName).HasColumnName("ScheduleName").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(200);
            Property(x => x.ScheduleType).HasColumnName("ScheduleType").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(100);
            Property(x => x.NumberDaysPerWeek).HasColumnName("NumberDaysPerWeek").IsOptional().HasColumnType("float");
            Property(x => x.NumberHolidays).HasColumnName("NumberHolidays").IsOptional().HasColumnType("int");
            Property(x => x.NumberWeeksPerYear).HasColumnName("NumberWeeksPerYear").IsOptional().HasColumnType("int");

            // Foreign keys
            HasRequired(a => a.BuildingEquipment).WithMany(b => b.BuildingEquipmentSchedules).HasForeignKey(c => c.BuildingEquipmentCid); // FK_BuildingEquipmentSchedule_BuildingEquipment
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingEquipmentScheduleDuration
    internal partial class BuildingEquipmentScheduleDurationMap : EntityTypeConfiguration<BuildingEquipmentScheduleDuration>
    {
        public BuildingEquipmentScheduleDurationMap()
            : this("dbo")
        {
        }

        public BuildingEquipmentScheduleDurationMap(string schema)
        {
            ToTable(schema + ".BuildingEquipmentScheduleDuration");
            HasKey(x => x.BuildingEquipmentScheduleDurationGuid);

            Property(x => x.BuildingEquipmentScheduleDurationGuid).HasColumnName("BuildingEquipmentScheduleDurationGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingEquipmentScheduleCid).HasColumnName("BuildingEquipmentScheduleCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.StartTicks).HasColumnName("StartTicks").IsOptional().HasColumnType("bigint");
            Property(x => x.EndTicks).HasColumnName("EndTicks").IsOptional().HasColumnType("bigint");
            Property(x => x.DurationType).HasColumnName("DurationType").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.StartTime).HasColumnName("StartTime").IsOptional().HasColumnType("time");
            Property(x => x.EndTime).HasColumnName("EndTime").IsOptional().HasColumnType("time");

            // Foreign keys
            HasRequired(a => a.BuildingEquipmentSchedule).WithMany(b => b.BuildingEquipmentScheduleDurations).HasForeignKey(c => c.BuildingEquipmentScheduleCid); // FK_BuildingEquipmentScheduleDuration_BuildingEquipmentSchedule
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingGasHistory
    internal partial class BuildingGasHistoryMap : EntityTypeConfiguration<BuildingGasHistory>
    {
        public BuildingGasHistoryMap()
            : this("dbo")
        {
        }

        public BuildingGasHistoryMap(string schema)
        {
            ToTable(schema + ".BuildingGasHistory");
            HasKey(x => new { x.BuildingGuid, x.GasHistoryReadDate });

            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.GasHistoryReadDate).HasColumnName("GasHistoryReadDate").IsRequired().HasColumnType("date").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Therms).HasColumnName("Therms").IsOptional().HasColumnType("float");
            Property(x => x.TotalBill).HasColumnName("TotalBill").IsOptional().HasColumnType("float");
            Property(x => x.CoolDegreeDays).HasColumnName("CoolDegreeDays").IsOptional().HasColumnType("int");
            Property(x => x.HeatDegreeDays).HasColumnName("HeatDegreeDays").IsOptional().HasColumnType("int");
            Property(x => x.BillingDays).HasColumnName("BillingDays").IsOptional().HasColumnType("int");

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.BuildingGasHistories).HasForeignKey(c => c.BuildingGuid); // FK_BuildingGasHistory_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingSpace
    internal partial class BuildingSpaceMap : EntityTypeConfiguration<BuildingSpace>
    {
        public BuildingSpaceMap()
            : this("dbo")
        {
        }

        public BuildingSpaceMap(string schema)
        {
            ToTable(schema + ".BuildingSpace");
            HasKey(x => x.BuildingSpaceCid);

            Property(x => x.BuildingSpaceCid).HasColumnName("BuildingSpaceCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingSpaceMfid).HasColumnName("BuildingSpaceMFID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.SpaceCode).HasColumnName("SpaceCode").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.SpaceLabel).HasColumnName("SpaceLabel").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Note).HasColumnName("Note").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.BuildingSpaces).HasForeignKey(c => c.BuildingGuid); // FK_BuildingSpace_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingType
    internal partial class BuildingTypeMap : EntityTypeConfiguration<BuildingType>
    {
        public BuildingTypeMap()
            : this("dbo")
        {
        }

        public BuildingTypeMap(string schema)
        {
            ToTable(schema + ".BuildingType");
            HasKey(x => new { x.BuildingCategory, x.BuildingType_ });

            Property(x => x.BuildingCategory).HasColumnName("BuildingCategory").IsRequired().HasColumnType("nvarchar").HasMaxLength(50).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingType_).HasColumnName("BuildingType").IsRequired().HasColumnType("nvarchar").HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingTypeId).HasColumnName("BuildingTypeId").IsRequired().HasColumnType("int");
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // BuildingUnitType
    internal partial class BuildingUnitTypeMap : EntityTypeConfiguration<BuildingUnitType>
    {
        public BuildingUnitTypeMap()
            : this("dbo")
        {
        }

        public BuildingUnitTypeMap(string schema)
        {
            ToTable(schema + ".BuildingUnitType");
            HasKey(x => x.BuildingUnitTypeId);

            Property(x => x.BuildingUnitTypeId).HasColumnName("BuildingUnitTypeId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.MultiFamilyId).HasColumnName("MultiFamilyId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.UnitType).HasColumnName("UnitType").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.UnitTypeName).HasColumnName("UnitTypeName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.UnitTypeQuantity).HasColumnName("UnitTypeQuantity").IsRequired().HasColumnType("int");
            Property(x => x.SquareFeet).HasColumnName("SquareFeet").IsRequired().HasColumnType("float");
            Property(x => x.NumBedrooms).HasColumnName("NumBedrooms").IsRequired().HasColumnType("int");
            Property(x => x.NumBathrooms).HasColumnName("NumBathrooms").IsRequired().HasColumnType("float");
            Property(x => x.HeatingType).HasColumnName("HeatingType").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.CoolingType).HasColumnName("CoolingType").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Location).HasColumnName("Location").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.DiOpportunity).HasColumnName("DIOpportunity").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Inventory).HasColumnName("Inventory").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.AdditionalDetails).HasColumnName("AdditionalDetails").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.MultiFamily).WithMany(b => b.BuildingUnitTypes).HasForeignKey(c => c.MultiFamilyId); // FK_BuildingUnitType_MultiFamily
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Company
    internal partial class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
            : this("dbo")
        {
        }

        public CompanyMap(string schema)
        {
            ToTable(schema + ".Company");
            HasKey(x => x.CompanyBsid);

            Property(x => x.CompanyBsid).HasColumnName("CompanyBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Address1).HasColumnName("Address1").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address2).HasColumnName("Address2").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address3).HasColumnName("Address3").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.State).HasColumnName("State").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.Zip).HasColumnName("Zip").IsOptional().HasColumnType("nvarchar").HasMaxLength(6);
            Property(x => x.ZipExt).HasColumnName("ZipExt").IsOptional().HasColumnType("nvarchar").HasMaxLength(4);
            Property(x => x.ElectricAccountNumber).HasColumnName("ElectricAccountNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ElectricRateCode).HasColumnName("ElectricRateCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.GasAccountNumber).HasColumnName("GasAccountNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.GasRateCode).HasColumnName("GasRateCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.RecordType).HasColumnName("RecordType").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.EmailAddress).HasColumnName("EmailAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Contact
    internal partial class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
            : this("dbo")
        {
        }

        public ContactMap(string schema)
        {
            ToTable(schema + ".Contact");
            HasKey(x => x.ContactGuid);

            Property(x => x.ContactGuid).HasColumnName("ContactGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyBsid).HasColumnName("CompanyBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.FirstName).HasColumnName("FirstName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.MiddleName).HasColumnName("MiddleName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.LastName).HasColumnName("LastName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Note).HasColumnName("Note").IsOptional().HasColumnType("nvarchar");
            Property(x => x.JobRole).HasColumnName("JobRole").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.EmailAddress).HasColumnName("EmailAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.Company).WithMany(b => b.Contacts).HasForeignKey(c => c.CompanyBsid); // FK_Contact_Company
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIAccount
    internal partial class DiAccountMap : EntityTypeConfiguration<DiAccount>
    {
        public DiAccountMap()
            : this("dbo")
        {
        }

        public DiAccountMap(string schema)
        {
            ToTable(schema + ".DIAccount");
            HasKey(x => x.DiAccountBsid);

            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Address1).HasColumnName("Address1").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address2).HasColumnName("Address2").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Address3).HasColumnName("Address3").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.City).HasColumnName("City").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.State).HasColumnName("State").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.Zip).HasColumnName("Zip").IsOptional().HasColumnType("nvarchar").HasMaxLength(6);
            Property(x => x.ZipExt).HasColumnName("ZipExt").IsOptional().HasColumnType("nvarchar").HasMaxLength(4);
            Property(x => x.ElectricAccountNumber).HasColumnName("ElectricAccountNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ElectricRateCode).HasColumnName("ElectricRateCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.GasAccountNumber).HasColumnName("GasAccountNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.GasRateCode).HasColumnName("GasRateCode").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.RecordType).HasColumnName("RecordType").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.EmailAddress).HasColumnName("EmailAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIAnswer
    internal partial class DiAnswerMap : EntityTypeConfiguration<DiAnswer>
    {
        public DiAnswerMap()
            : this("dbo")
        {
        }

        public DiAnswerMap(string schema)
        {
            ToTable(schema + ".DIAnswer");
            HasKey(x => new { x.DiQuestionId, x.DiProjectInfoId });

            Property(x => x.DiQuestionId).HasColumnName("DIQuestionID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AnsweredOn).HasColumnName("AnsweredOn").IsRequired().HasColumnType("datetime");
            Property(x => x.Value).HasColumnName("Value").IsOptional().HasColumnType("nvarchar");

            // Foreign keys
            HasRequired(a => a.DiProjectInfo).WithMany(b => b.DiAnswers).HasForeignKey(c => c.DiProjectInfoId); // FK_DIAnswer_DIProjectInfo
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIAttachment
    internal partial class DiAttachmentMap : EntityTypeConfiguration<DiAttachment>
    {
        public DiAttachmentMap()
            : this("dbo")
        {
        }

        public DiAttachmentMap(string schema)
        {
            ToTable(schema + ".DIAttachment");
            HasKey(x => x.DiAttachmentId);

            Property(x => x.DiAttachmentId).HasColumnName("DIAttachmentID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.RestoreUrl).HasColumnName("RestoreUrl").IsOptional().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.Comments).HasColumnName("Comments").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.AttachmentFile).HasColumnName("AttachmentFile").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Category).HasColumnName("Category").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.IncludeInReport).HasColumnName("IncludeInReport").IsOptional().HasColumnType("bit");
            Property(x => x.IncludeOnCover).HasColumnName("IncludeOnCover").IsOptional().HasColumnType("bit");

            // Foreign keys
            HasRequired(a => a.DiProjectInfo).WithMany(b => b.DiAttachments).HasForeignKey(c => c.DiProjectInfoId); // FK_DIAttachment_DIProjectInfo
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIContact
    internal partial class DiContactMap : EntityTypeConfiguration<DiContact>
    {
        public DiContactMap()
            : this("dbo")
        {
        }

        public DiContactMap(string schema)
        {
            ToTable(schema + ".DIContact");
            HasKey(x => x.DiContactGuid);

            Property(x => x.DiContactGuid).HasColumnName("DIContactGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.FirstName).HasColumnName("FirstName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.MiddleName).HasColumnName("MiddleName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.LastName).HasColumnName("LastName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Note).HasColumnName("Note").IsOptional().HasColumnType("nvarchar");
            Property(x => x.JobRole).HasColumnName("JobRole").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.EmailAddress).HasColumnName("EmailAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.DiAccount).WithMany(b => b.DiContacts).HasForeignKey(c => c.DiAccountBsid); // FK_DIContact_DIAccount
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIElectricHistory
    internal partial class DiElectricHistoryMap : EntityTypeConfiguration<DiElectricHistory>
    {
        public DiElectricHistoryMap()
            : this("dbo")
        {
        }

        public DiElectricHistoryMap(string schema)
        {
            ToTable(schema + ".DIElectricHistory");
            HasKey(x => new { x.DiAccountBsid, x.ElectricHistoryReadDate });

            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ElectricHistoryReadDate).HasColumnName("ElectricHistoryReadDate").IsRequired().HasColumnType("date").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BillDays).HasColumnName("BillDays").IsOptional().HasColumnType("int");
            Property(x => x.CoolDays).HasColumnName("CoolDays").IsOptional().HasColumnType("int");
            Property(x => x.HeatDays).HasColumnName("HeatDays").IsOptional().HasColumnType("int");
            Property(x => x.TotalKwh).HasColumnName("TotalKwh").IsOptional().HasColumnType("float");
            Property(x => x.OffPeakKwh).HasColumnName("OffPeakKwh").IsOptional().HasColumnType("float");
            Property(x => x.OnPeakKwh).HasColumnName("OnPeakKwh").IsOptional().HasColumnType("float");
            Property(x => x.BilledKw).HasColumnName("BilledKw").IsOptional().HasColumnType("float");
            Property(x => x.MaximumCustomerKw).HasColumnName("MaximumCustomerKw").IsOptional().HasColumnType("float");
            Property(x => x.TotalBill).HasColumnName("TotalBill").IsOptional().HasColumnType("float");

            // Foreign keys
            HasRequired(a => a.DiAccount).WithMany(b => b.DiElectricHistories).HasForeignKey(c => c.DiAccountBsid); // FK_DIElectricHistory_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIGasHistory
    internal partial class DiGasHistoryMap : EntityTypeConfiguration<DiGasHistory>
    {
        public DiGasHistoryMap()
            : this("dbo")
        {
        }

        public DiGasHistoryMap(string schema)
        {
            ToTable(schema + ".DIGasHistory");
            HasKey(x => new { x.DiAccountBsid, x.GasHistoryReadDate });

            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.GasHistoryReadDate).HasColumnName("GasHistoryReadDate").IsRequired().HasColumnType("date").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Therms).HasColumnName("Therms").IsOptional().HasColumnType("float");
            Property(x => x.TotalBill).HasColumnName("TotalBill").IsOptional().HasColumnType("float");
            Property(x => x.CoolDegreeDays).HasColumnName("CoolDegreeDays").IsOptional().HasColumnType("int");
            Property(x => x.HeatDegreeDays).HasColumnName("HeatDegreeDays").IsOptional().HasColumnType("int");
            Property(x => x.BillingDays).HasColumnName("BillingDays").IsOptional().HasColumnType("int");

            // Foreign keys
            HasRequired(a => a.DiAccount).WithMany(b => b.DiGasHistories).HasForeignKey(c => c.DiAccountBsid); // FK_DIGasHistory_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIInkSecureSignatureData
    internal partial class DiInkSecureSignatureDataMap : EntityTypeConfiguration<DiInkSecureSignatureData>
    {
        public DiInkSecureSignatureDataMap()
            : this("dbo")
        {
        }

        public DiInkSecureSignatureDataMap(string schema)
        {
            ToTable(schema + ".DIInkSecureSignatureData");
            HasKey(x => x.DiProjectInfoId);

            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AcquiredSignatureStartOn).HasColumnName("AcquiredSignatureStartOn").IsRequired().HasColumnType("datetime");
            Property(x => x.BiometricEncryptionCompletedOn).HasColumnName("BiometricEncryptionCompletedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.BiometricEncryptionSubmittedOn).HasColumnName("BiometricEncryptionSubmittedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.EncryptedBiometricData).HasColumnName("EncryptedBiometricData").IsRequired().HasColumnType("nvarchar");
            Property(x => x.HardwareInfo).HasColumnName("HardwareInfo").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.InkWashedSignature).HasColumnName("InkWashedSignature").IsRequired().HasColumnType("nvarchar");
            Property(x => x.SignerAcceptedOn).HasColumnName("SignerAcceptedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.SignersName).HasColumnName("SignersName").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.DiProjectInfo).WithOptional(b => b.DiInkSecureSignatureData); // FK_DIInkSecureSignatureData_AuditProject
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIProjectInfo
    internal partial class DiProjectInfoMap : EntityTypeConfiguration<DiProjectInfo>
    {
        public DiProjectInfoMap()
            : this("dbo")
        {
        }

        public DiProjectInfoMap(string schema)
        {
            ToTable(schema + ".DIProjectInfo");
            HasKey(x => x.DiProjectInfoId);

            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ExternalId).HasColumnName("ExternalId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProgramId).HasColumnName("ProgramID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(10);
            Property(x => x.ProjectCompleteDate).HasColumnName("ProjectCompleteDate").IsOptional().HasColumnType("datetime");
            Property(x => x.ProjectName).HasColumnName("ProjectName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.EnergyAdvisorName).HasColumnName("EnergyAdvisorName").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ActualStartTime).HasColumnName("ActualStartTime").IsOptional().HasColumnType("datetime");
            Property(x => x.ScheduledStartTime).HasColumnName("ScheduledStartTime").IsOptional().HasColumnType("datetime");
            Property(x => x.IsReportEmailSent).HasColumnName("IsReportEmailSent").IsOptional().HasColumnType("bit");

            // Foreign keys
            HasRequired(a => a.DiAccount).WithMany(b => b.DiProjectInfoes).HasForeignKey(c => c.DiAccountBsid); // FK_DIProjectInfo_DIAccount
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIRetrofit
    internal partial class DiRetrofitMap : EntityTypeConfiguration<DiRetrofit>
    {
        public DiRetrofitMap()
            : this("dbo")
        {
        }

        public DiRetrofitMap(string schema)
        {
            ToTable(schema + ".DIRetrofit");
            HasKey(x => x.DiRetrofitGuid);

            Property(x => x.DiRetrofitGuid).HasColumnName("DIRetrofitGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ComponentBsid).HasColumnName("ComponentBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProgramId).HasColumnName("ProgramID").IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("float");
            Property(x => x.Kwh).HasColumnName("Kwh").IsOptional().HasColumnType("float");
            Property(x => x.Therms).HasColumnName("Therms").IsOptional().HasColumnType("float");
            Property(x => x.Water).HasColumnName("Water").IsOptional().HasColumnType("float");
            Property(x => x.Savings).HasColumnName("Savings").IsOptional().HasColumnType("decimal");
            Property(x => x.Space).HasColumnName("Space").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Incentive).HasColumnName("Incentive").IsOptional().HasColumnType("float");
            Property(x => x.IconPath).HasColumnName("IconPath").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.KWhUnit).HasColumnName("KWhUnit").IsOptional().HasColumnType("float");
            Property(x => x.ThermsUnit).HasColumnName("ThermsUnit").IsOptional().HasColumnType("float");
            Property(x => x.IconFileName).HasColumnName("IconFileName").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.LineItem1).HasColumnName("LineItem1").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.LineItem2).HasColumnName("LineItem2").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.LineItem3).HasColumnName("LineItem3").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.LineItem4).HasColumnName("LineItem4").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.DiProjectInfo).WithMany(b => b.DiRetrofits).HasForeignKey(c => c.DiProjectInfoId); // FK_DIRetrofit_DIProjectInfo
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // DIUploadBackup
    internal partial class DiUploadBackupMap : EntityTypeConfiguration<DiUploadBackup>
    {
        public DiUploadBackupMap()
            : this("dbo")
        {
        }

        public DiUploadBackupMap(string schema)
        {
            ToTable(schema + ".DIUploadBackup");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("bigint").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UploadedBy).HasColumnName("UploadedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.UploadedDate).HasColumnName("UploadedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.DiAccountBsid).HasColumnName("DIAccountBSID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.DiProjectInfoId).HasColumnName("DIProjectInfoID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.DiId).HasColumnName("DIId").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.DiDataXml).HasColumnName("DIDataXml").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ErrorMessage).HasColumnName("ErrorMessage").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ErrorObject).HasColumnName("ErrorObject").IsOptional().HasColumnType("nvarchar");
            Property(x => x.DiDataUrl).HasColumnName("DIDataUrl").IsOptional().HasColumnType("nvarchar").HasMaxLength(300);
            Property(x => x.IsDeletedDi).HasColumnName("IsDeletedDi").IsRequired().HasColumnType("bit");
            Property(x => x.ExternalId).HasColumnName("ExternalId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);

            // Foreign keys
            HasOptional(a => a.DiAccount).WithMany(b => b.DiUploadBackups).HasForeignKey(c => c.DiAccountBsid); // FK_DIUploadBackup_DIAccount
            HasOptional(a => a.DiProjectInfo).WithMany(b => b.DiUploadBackups).HasForeignKey(c => c.DiProjectInfoId); // FK_DIUploadBackup_DIProject
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // EmailRequestLog
    internal partial class EmailRequestLogMap : EntityTypeConfiguration<EmailRequestLog>
    {
        public EmailRequestLogMap()
            : this("dbo")
        {
        }

        public EmailRequestLogMap(string schema)
        {
            ToTable(schema + ".EmailRequestLog");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DateTime).HasColumnName("DateTime").IsRequired().HasColumnType("datetime2");
            Property(x => x.CompanyBsid).HasColumnName("CompanyBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.EmailSent).HasColumnName("EmailSent").IsOptional().HasColumnType("bit");
            Property(x => x.ToAddress).HasColumnName("ToAddress").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.RequestedBy).HasColumnName("RequestedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.Company).WithMany(b => b.EmailRequestLogs).HasForeignKey(c => c.CompanyBsid); // FK_EmailRequestLog_Company
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // InkSecureSignatureData
    internal partial class InkSecureSignatureDataMap : EntityTypeConfiguration<InkSecureSignatureData>
    {
        public InkSecureSignatureDataMap()
            : this("dbo")
        {
        }

        public InkSecureSignatureDataMap(string schema)
        {
            ToTable(schema + ".InkSecureSignatureData");
            HasKey(x => x.AuditProjectBsid);

            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AcquiredSignatureStartOn).HasColumnName("AcquiredSignatureStartOn").IsRequired().HasColumnType("datetime");
            Property(x => x.BiometricEncryptionCompletedOn).HasColumnName("BiometricEncryptionCompletedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.BiometricEncryptionSubmittedOn).HasColumnName("BiometricEncryptionSubmittedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.EncryptedBiometricData).HasColumnName("EncryptedBiometricData").IsRequired().HasColumnType("nvarchar");
            Property(x => x.HardwareInfo).HasColumnName("HardwareInfo").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.InkWashedSignature).HasColumnName("InkWashedSignature").IsRequired().HasColumnType("nvarchar");
            Property(x => x.SignerAcceptedOn).HasColumnName("SignerAcceptedOn").IsRequired().HasColumnType("datetime");
            Property(x => x.SignersName).HasColumnName("SignersName").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.AuditProject).WithOptional(b => b.InkSecureSignatureData); // FK_InkSecureSignatureData_AuditProject
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // MultiFamily
    internal partial class MultiFamilyMap : EntityTypeConfiguration<MultiFamily>
    {
        public MultiFamilyMap()
            : this("dbo")
        {
        }

        public MultiFamilyMap(string schema)
        {
            ToTable(schema + ".MultiFamily");
            HasKey(x => x.MultiFamilyId);

            Property(x => x.MultiFamilyId).HasColumnName("MultiFamilyId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);

            // Foreign keys
            HasRequired(a => a.Building).WithMany(b => b.MultiFamilies).HasForeignKey(c => c.BuildingGuid); // FK_MultiFamily_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Recommendation
    internal partial class RecommendationMap : EntityTypeConfiguration<Recommendation>
    {
        public RecommendationMap()
            : this("dbo")
        {
        }

        public RecommendationMap(string schema)
        {
            ToTable(schema + ".Recommendation");
            HasKey(x => x.InstanceId);

            Property(x => x.RecommendationGuid).HasColumnName("RecommendationGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.RecommendationName).HasColumnName("RecommendationName").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.RecommendationDescription).HasColumnName("RecommendationDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.IncludedInReport).HasColumnName("IncludedInReport").IsOptional().HasColumnType("bit");
            Property(x => x.ReportRank).HasColumnName("ReportRank").IsOptional().HasColumnType("int");
            Property(x => x.IsOccupancySensor).HasColumnName("IsOccupancySensor").IsOptional().HasColumnType("bit");
            Property(x => x.InstanceId).HasColumnName("InstanceId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Foreign keys
            HasRequired(a => a.AuditProject).WithMany(b => b.Recommendations).HasForeignKey(c => c.AuditProjectBsid); // FK_Recommendation_AuditProject
            HasRequired(a => a.Building).WithMany(b => b.Recommendations).HasForeignKey(c => c.BuildingGuid); // FK_Recommendation_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RecommendationOption
    internal partial class RecommendationOptionMap : EntityTypeConfiguration<RecommendationOption>
    {
        public RecommendationOptionMap()
            : this("dbo")
        {
        }

        public RecommendationOptionMap(string schema)
        {
            ToTable(schema + ".RecommendationOption");
            HasKey(x => x.RecommendationOptionGuid);

            Property(x => x.RecommendationOptionGuid).HasColumnName("RecommendationOptionGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RecommendationGuid).HasColumnName("RecommendationGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.RecommendationName).HasColumnName("RecommendationName").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.OptionDescription).HasColumnName("OptionDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.OptionId).HasColumnName("OptionId").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.OptionName).HasColumnName("OptionName").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Cop).HasColumnName("Cop").IsOptional().HasColumnType("float");
            Property(x => x.ElectricDisplayAs).HasColumnName("ElectricDisplayAs").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.EnergyFactor).HasColumnName("EnergyFactor").IsOptional().HasColumnType("float");
            Property(x => x.EnergySource).HasColumnName("EnergySource").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.GasDisplayAs).HasColumnName("GasDisplayAs").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.HeatingCoolingHours).HasColumnName("HeatingCoolingHours").IsOptional().HasColumnType("float");
            Property(x => x.IsOccupancySensor).HasColumnName("IsOccupancySensor").IsOptional().HasColumnType("bit");
            Property(x => x.IsZeroSavings).HasColumnName("IsZeroSavings").IsOptional().HasColumnType("bit");
            Property(x => x.KwhSaved).HasColumnName("KwhSaved").IsOptional().HasColumnType("float");
            Property(x => x.KwhSavedWithRateCode).HasColumnName("KwhSavedWithRateCode").IsOptional().HasColumnType("float");
            Property(x => x.OccupancySensorDisplayAs).HasColumnName("OccupancySensorDisplayAs").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.OriginalEquipmentMfid).HasColumnName("OriginalEquipmentMFID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("Quantity").IsOptional().HasColumnType("int");
            Property(x => x.RebateBsid).HasColumnName("RebateBSID").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.RebateCalculationEquation).HasColumnName("RebateCalculationEquation").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.RebateClientRefId).HasColumnName("RebateClientRefID").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.RebateValue).HasColumnName("RebateValue").IsOptional().HasColumnType("float");
            Property(x => x.Savings).HasColumnName("Savings").IsOptional().HasColumnType("float");
            Property(x => x.SavingsCalculationEquationSaving).HasColumnName("SavingsCalculationEquationSaving").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.ThermsSaved).HasColumnName("ThermsSaved").IsOptional().HasColumnType("float");
            Property(x => x.ThermsSavedWithRateCode).HasColumnName("ThermsSavedWithRateCode").IsOptional().HasColumnType("float");
            Property(x => x.TypeOfEnergy).HasColumnName("TypeOfEnergy").IsOptional().HasColumnType("nvarchar").HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.Recommendation).WithMany(b => b.RecommendationOptions).HasForeignKey(c => c.RecommendationGuid); // FK_RecommendationOption_Recommendation
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RecommendationOptionEquipment
    internal partial class RecommendationOptionEquipmentMap : EntityTypeConfiguration<RecommendationOptionEquipment>
    {
        public RecommendationOptionEquipmentMap()
            : this("dbo")
        {
        }

        public RecommendationOptionEquipmentMap(string schema)
        {
            ToTable(schema + ".RecommendationOptionEquipment");
            HasKey(x => x.RecommendationOptionEquipmentCid);

            Property(x => x.RecommendationOptionEquipmentCid).HasColumnName("RecommendationOptionEquipmentCID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RecommendationOptionEquipmentMfid).HasColumnName("RecommendationOptionEquipmentMFID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.RecommendationOptionGuid).HasColumnName("RecommendationOptionGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ActualWattage).HasColumnName("ActualWattage").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.AnnualHours).HasColumnName("AnnualHours").IsOptional().HasColumnType("float");
            Property(x => x.Efficiency).HasColumnName("Efficiency").IsOptional().HasColumnType("float");
            Property(x => x.EfficiencyUnit).HasColumnName("EfficiencyUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.EfficiencyDescription).HasColumnName("EfficiencyDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.EquipmentDescription).HasColumnName("EquipmentDescription").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.EquipmentName).HasColumnName("EquipmentName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Quantity).HasColumnName("Quantity").IsOptional().HasColumnType("float");
            Property(x => x.RecommendationEquipmentId).HasColumnName("RecommendationEquipmentId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Size).HasColumnName("Size").IsOptional().HasColumnType("float");
            Property(x => x.SizeUnit).HasColumnName("SizeUnit").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.SystemType).HasColumnName("SystemType").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.ThermalEfficiency).HasColumnName("ThermalEfficiency").IsOptional().HasColumnType("float");

            // Foreign keys
            HasRequired(a => a.RecommendationOption).WithMany(b => b.RecommendationOptionEquipments).HasForeignKey(c => c.RecommendationOptionGuid); // FK_RecommendationOptionEquipment_RecommendationOption
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // ReportTracking
    internal partial class ReportTrackingMap : EntityTypeConfiguration<ReportTracking>
    {
        public ReportTrackingMap()
            : this("dbo")
        {
        }

        public ReportTrackingMap(string schema)
        {
            ToTable(schema + ".ReportTracking");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.AccountId).HasColumnName("AccountId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProjectId).HasColumnName("ProjectId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.UploadedBy).HasColumnName("UploadedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.UploadedDateTime).HasColumnName("UploadedDateTime").IsRequired().HasColumnType("datetime2");
            Property(x => x.Url).HasColumnName("Url").IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // Retrofit
    internal partial class RetrofitMap : EntityTypeConfiguration<Retrofit>
    {
        public RetrofitMap()
            : this("dbo")
        {
        }

        public RetrofitMap(string schema)
        {
            ToTable(schema + ".Retrofit");
            HasKey(x => x.RetrofitGuid);

            Property(x => x.RetrofitGuid).HasColumnName("RetrofitGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingGuid).HasColumnName("BuildingGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.AuditProjectBsid).HasColumnName("AuditProjectBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ComponentBsid).HasColumnName("ComponentBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProgramId).HasColumnName("ProgramID").IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("int");
            Property(x => x.Kwh).HasColumnName("Kwh").IsOptional().HasColumnType("float");
            Property(x => x.Therms).HasColumnName("Therms").IsOptional().HasColumnType("float");
            Property(x => x.Water).HasColumnName("Water").IsOptional().HasColumnType("float");
            Property(x => x.Savings).HasColumnName("Savings").IsOptional().HasColumnType("decimal");
            Property(x => x.Space).HasColumnName("Space").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Incentive).HasColumnName("Incentive").IsOptional().HasColumnType("float");
            Property(x => x.IconPath).HasColumnName("IconPath").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.AuditProject).WithMany(b => b.Retrofits).HasForeignKey(c => c.AuditProjectBsid); // FK_Retrofit_AuditProject
            HasRequired(a => a.Building).WithMany(b => b.Retrofits).HasForeignKey(c => c.BuildingGuid); // FK_Retrofit_Building
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RetrofitEstimate
    internal partial class RetrofitEstimateMap : EntityTypeConfiguration<RetrofitEstimate>
    {
        public RetrofitEstimateMap()
            : this("dbo")
        {
        }

        public RetrofitEstimateMap(string schema)
        {
            ToTable(schema + ".RetrofitEstimate");
            HasKey(x => x.RetrofitGuid);

            Property(x => x.RetrofitGuid).HasColumnName("RetrofitGUID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BuildingUnitTypeId).HasColumnName("BuildingUnitTypeId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ComponentBsid).HasColumnName("ComponentBSID").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProgramId).HasColumnName("ProgramID").IsRequired().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired().HasColumnType("int");
            Property(x => x.Kwh).HasColumnName("Kwh").IsOptional().HasColumnType("float");
            Property(x => x.Therms).HasColumnName("Therms").IsOptional().HasColumnType("float");
            Property(x => x.Water).HasColumnName("Water").IsOptional().HasColumnType("float");
            Property(x => x.Savings).HasColumnName("Savings").IsOptional().HasColumnType("decimal");
            Property(x => x.Space).HasColumnName("Space").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.Incentive).HasColumnName("Incentive").IsOptional().HasColumnType("float");
            Property(x => x.IconPath).HasColumnName("IconPath").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.BuildingUnitType).WithMany(b => b.RetrofitEstimates).HasForeignKey(c => c.BuildingUnitTypeId); // FK_RetrofitEstimate_BuildingUnitType
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // database_firewall_rules
    internal partial class sys_DatabaseFirewallRuleMap : EntityTypeConfiguration<sys_DatabaseFirewallRule>
    {
        public sys_DatabaseFirewallRuleMap()
            : this("sys")
        {
        }

        public sys_DatabaseFirewallRuleMap(string schema)
        {
            ToTable(schema + ".database_firewall_rules");
            HasKey(x => new { x.Id, x.Name, x.StartIpAddress, x.EndIpAddress, x.CreateDate, x.ModifyDate });

            Property(x => x.Id).HasColumnName("id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("name").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.StartIpAddress).HasColumnName("start_ip_address").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(45);
            Property(x => x.EndIpAddress).HasColumnName("end_ip_address").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(45);
            Property(x => x.CreateDate).HasColumnName("create_date").IsRequired().HasColumnType("datetime");
            Property(x => x.ModifyDate).HasColumnName("modify_date").IsRequired().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // script_deployments
    internal partial class sys_ScriptDeploymentMap : EntityTypeConfiguration<sys_ScriptDeployment>
    {
        public sys_ScriptDeploymentMap()
            : this("sys")
        {
        }

        public sys_ScriptDeploymentMap(string schema)
        {
            ToTable(schema + ".script_deployments");
            HasKey(x => new { x.DeploymentId, x.CoordinatorId, x.DeploymentName, x.DeploymentSubmitted, x.Status, x.RetryPolicy, x.Script });

            Property(x => x.DeploymentId).HasColumnName("deployment_id").IsRequired().HasColumnType("uniqueidentifier");
            Property(x => x.CoordinatorId).HasColumnName("coordinator_id").IsRequired().HasColumnType("uniqueidentifier");
            Property(x => x.DeploymentName).HasColumnName("deployment_name").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.DeploymentSubmitted).HasColumnName("deployment_submitted").IsRequired().HasColumnType("datetimeoffset");
            Property(x => x.DeploymentStart).HasColumnName("deployment_start").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.DeploymentEnd).HasColumnName("deployment_end").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.Status).HasColumnName("status").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.ResultsTable).HasColumnName("results_table").IsOptional().HasColumnType("nvarchar").HasMaxLength(261);
            Property(x => x.RetryPolicy).HasColumnName("retry_policy").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.Script).HasColumnName("script").IsRequired().HasColumnType("nvarchar");
            Property(x => x.Messages).HasColumnName("messages").IsOptional().HasColumnType("nvarchar");
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // script_deployment_status
    internal partial class sys_ScriptDeploymentStatusMap : EntityTypeConfiguration<sys_ScriptDeploymentStatus>
    {
        public sys_ScriptDeploymentStatusMap()
            : this("sys")
        {
        }

        public sys_ScriptDeploymentStatusMap(string schema)
        {
            ToTable(schema + ".script_deployment_status");
            HasKey(x => new { x.DeploymentId, x.LogicalServer, x.DatabaseName, x.Status, x.NumRetries });

            Property(x => x.DeploymentId).HasColumnName("deployment_id").IsRequired().HasColumnType("uniqueidentifier");
            Property(x => x.WorkerId).HasColumnName("worker_id").IsOptional().HasColumnType("uniqueidentifier");
            Property(x => x.LogicalServer).HasColumnName("logical_server").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.DatabaseName).HasColumnName("database_name").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.DeploymentStart).HasColumnName("deployment_start").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.DeploymentEnd).HasColumnName("deployment_end").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.Status).HasColumnName("status").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.NumRetries).HasColumnName("num_retries").IsRequired().HasColumnType("smallint");
            Property(x => x.Messages).HasColumnName("messages").IsOptional().HasColumnType("nvarchar");
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // UploadedFile
    internal partial class UploadedFileMap : EntityTypeConfiguration<UploadedFile>
    {
        public UploadedFileMap()
            : this("dbo")
        {
        }

        public UploadedFileMap(string schema)
        {
            ToTable(schema + ".UploadedFile");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Type).HasColumnName("Type").IsOptional().HasColumnType("nvarchar").HasMaxLength(10);
            Property(x => x.AccountId).HasColumnName("AccountId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ProjectId).HasColumnName("ProjectId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.UploadedBy).HasColumnName("UploadedBy").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.UploadedDateTime).HasColumnName("UploadedDateTime").IsRequired().HasColumnType("datetime2");
            Property(x => x.Url).HasColumnName("Url").IsRequired().HasColumnType("nvarchar").HasMaxLength(400);
            Property(x => x.UploadedFileType).HasColumnName("UploadedFileType").IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }


    // ************************************************************************
    // Stored procedure return models

}

