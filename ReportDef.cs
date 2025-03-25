/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Reports
{
    private ReportsReport[] reportField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Report")]
    public ReportsReport[] Report
    {
        get
        {
            return this.reportField;
        }
        set
        {
            this.reportField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReport
{
    private string nameField;

    private ReportsReportReportInfo reportInfoField;

    private ReportsReportDataSource dataSourceField;

    private ReportsReportLayout layoutField;

    private ReportsReportFont fontField;

    private object compatibilityOptionsField;

    private object fontProcessingOptionsField;

    private ReportsReportGroups groupsField;

    private ReportsReportSection[] sectionsField;

    private ReportsReportField[] fieldsField;

    private string versionField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public ReportsReportReportInfo ReportInfo
    {
        get
        {
            return this.reportInfoField;
        }
        set
        {
            this.reportInfoField = value;
        }
    }

    /// <remarks/>
    public ReportsReportDataSource DataSource
    {
        get
        {
            return this.dataSourceField;
        }
        set
        {
            this.dataSourceField = value;
        }
    }

    /// <remarks/>
    public ReportsReportLayout Layout
    {
        get
        {
            return this.layoutField;
        }
        set
        {
            this.layoutField = value;
        }
    }

    /// <remarks/>
    public ReportsReportFont Font
    {
        get
        {
            return this.fontField;
        }
        set
        {
            this.fontField = value;
        }
    }

    /// <remarks/>
    public object CompatibilityOptions
    {
        get
        {
            return this.compatibilityOptionsField;
        }
        set
        {
            this.compatibilityOptionsField = value;
        }
    }

    /// <remarks/>
    public object FontProcessingOptions
    {
        get
        {
            return this.fontProcessingOptionsField;
        }
        set
        {
            this.fontProcessingOptionsField = value;
        }
    }

    /// <remarks/>
    public ReportsReportGroups Groups
    {
        get
        {
            return this.groupsField;
        }
        set
        {
            this.groupsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Section", IsNullable = false)]
    public ReportsReportSection[] Sections
    {
        get
        {
            return this.sectionsField;
        }
        set
        {
            this.sectionsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Field", IsNullable = false)]
    public ReportsReportField[] Fields
    {
        get
        {
            return this.fieldsField;
        }
        set
        {
            this.fieldsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportReportInfo
{
    private string authorField;

    /// <remarks/>
    public string Author
    {
        get
        {
            return this.authorField;
        }
        set
        {
            this.authorField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportDataSource
{
    private string connectionStringField;

    private string recordSourceField;

    private byte dataProviderField;

    /// <remarks/>
    public string ConnectionString
    {
        get
        {
            return this.connectionStringField;
        }
        set
        {
            this.connectionStringField = value;
        }
    }

    /// <remarks/>
    public string RecordSource
    {
        get
        {
            return this.recordSourceField;
        }
        set
        {
            this.recordSourceField = value;
        }
    }

    /// <remarks/>
    public byte DataProvider
    {
        get
        {
            return this.dataProviderField;
        }
        set
        {
            this.dataProviderField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportLayout
{
    private ushort widthField;

    private ushort marginLeftField;

    private bool marginLeftFieldSpecified;

    private ushort marginTopField;

    private bool marginTopFieldSpecified;

    private ushort marginBottomField;

    private bool marginBottomFieldSpecified;

    private byte orientationField;

    private byte paperSizeField;

    private bool paperSizeFieldSpecified;

    /// <remarks/>
    public ushort Width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    public ushort MarginLeft
    {
        get
        {
            return this.marginLeftField;
        }
        set
        {
            this.marginLeftField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MarginLeftSpecified
    {
        get
        {
            return this.marginLeftFieldSpecified;
        }
        set
        {
            this.marginLeftFieldSpecified = value;
        }
    }

    /// <remarks/>
    public ushort MarginTop
    {
        get
        {
            return this.marginTopField;
        }
        set
        {
            this.marginTopField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MarginTopSpecified
    {
        get
        {
            return this.marginTopFieldSpecified;
        }
        set
        {
            this.marginTopFieldSpecified = value;
        }
    }

    /// <remarks/>
    public ushort MarginBottom
    {
        get
        {
            return this.marginBottomField;
        }
        set
        {
            this.marginBottomField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MarginBottomSpecified
    {
        get
        {
            return this.marginBottomFieldSpecified;
        }
        set
        {
            this.marginBottomFieldSpecified = value;
        }
    }

    /// <remarks/>
    public byte Orientation
    {
        get
        {
            return this.orientationField;
        }
        set
        {
            this.orientationField = value;
        }
    }

    /// <remarks/>
    public byte PaperSize
    {
        get
        {
            return this.paperSizeField;
        }
        set
        {
            this.paperSizeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PaperSizeSpecified
    {
        get
        {
            return this.paperSizeFieldSpecified;
        }
        set
        {
            this.paperSizeFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportFont
{
    private string nameField;

    private byte sizeField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public byte Size
    {
        get
        {
            return this.sizeField;
        }
        set
        {
            this.sizeField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportGroups
{
    private ReportsReportGroupsGroup groupField;

    /// <remarks/>
    public ReportsReportGroupsGroup Group
    {
        get
        {
            return this.groupField;
        }
        set
        {
            this.groupField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportGroupsGroup
{
    private string nameField;

    private string groupByField;

    /// <remarks/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string GroupBy
    {
        get
        {
            return this.groupByField;
        }
        set
        {
            this.groupByField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportSection
{
    private object[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ForcePageBreak", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("Height", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("OnFormat", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("PrintAtPageBottom", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("Repeat", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("Type", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("Visible", typeof(byte))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType
{
    /// <remarks/>
    ForcePageBreak,

    /// <remarks/>
    Height,

    /// <remarks/>
    KeepTogether,

    /// <remarks/>
    Name,

    /// <remarks/>
    OnFormat,

    /// <remarks/>
    PrintAtPageBottom,

    /// <remarks/>
    Repeat,

    /// <remarks/>
    Type,

    /// <remarks/>
    Visible,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportField
{
    private object[] itemsField;

    private ItemsChoiceType1[] itemsElementNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Align", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("BackColor", typeof(uint))]
    [System.Xml.Serialization.XmlElementAttribute("BackStyle", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("BorderColor", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("BorderStyle", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("Calculated", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("CanGrow", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("CanShrink", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("Font", typeof(ReportsReportFieldFont))]
    [System.Xml.Serialization.XmlElementAttribute("ForcePageBreak", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("ForeColor", typeof(uint))]
    [System.Xml.Serialization.XmlElementAttribute("Format", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("Height", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("Left", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("Picture", typeof(ReportsReportFieldPicture))]
    [System.Xml.Serialization.XmlElementAttribute("RTF", typeof(sbyte))]
    [System.Xml.Serialization.XmlElementAttribute("Section", typeof(byte))]
    [System.Xml.Serialization.XmlElementAttribute("Subreport", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("Text", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("Top", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("Width", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("WordWrap", typeof(byte))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType1[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportFieldFont
{
    private sbyte boldField;

    private bool boldFieldSpecified;

    private string nameField;

    private byte sizeField;

    /// <remarks/>
    public sbyte Bold
    {
        get
        {
            return this.boldField;
        }
        set
        {
            this.boldField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool BoldSpecified
    {
        get
        {
            return this.boldFieldSpecified;
        }
        set
        {
            this.boldFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string Name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public byte Size
    {
        get
        {
            return this.sizeField;
        }
        set
        {
            this.sizeField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportsReportFieldPicture
{
    private string encodingField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string encoding
    {
        get
        {
            return this.encodingField;
        }
        set
        {
            this.encodingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType1
{
    /// <remarks/>
    Align,

    /// <remarks/>
    BackColor,

    /// <remarks/>
    BackStyle,

    /// <remarks/>
    BorderColor,

    /// <remarks/>
    BorderStyle,

    /// <remarks/>
    Calculated,

    /// <remarks/>
    CanGrow,

    /// <remarks/>
    CanShrink,

    /// <remarks/>
    Font,

    /// <remarks/>
    ForcePageBreak,

    /// <remarks/>
    ForeColor,

    /// <remarks/>
    Format,

    /// <remarks/>
    Height,

    /// <remarks/>
    KeepTogether,

    /// <remarks/>
    Left,

    /// <remarks/>
    Name,

    /// <remarks/>
    Picture,

    /// <remarks/>
    RTF,

    /// <remarks/>
    Section,

    /// <remarks/>
    Subreport,

    /// <remarks/>
    Text,

    /// <remarks/>
    Top,

    /// <remarks/>
    Width,

    /// <remarks/>
    WordWrap,
}