using PdfLibSharp.Drawing;
using PdfLibSharp.Elements;
using PdfLibSharp.Elements.Content;
using PdfLibSharp.Elements.Layout;

namespace PdfLibSharp.Tests;

public static class ComplexPdfBuilder
{
    private const string DefaultFontFamily = "Times New Roman";
    private const double DefaultFontSize = 12;
    
    public static Pdf GeneratePdf()
    {
        var pdf = new Pdf
        {
            DefaultFont = new Font(DefaultFontFamily, DefaultFontSize),
            DefaultPageSize = PageSize.A4,
            DefaultLineHeight = 1.2
        };

        IPage page = pdf.AddPage();

        page.Sizing = ElementSizing.ExpandToFillBounds;
        page.ElementAlignment = ElementAlignment.Stretch;

        page.StringFormat = StringFormat.TopLeft;
        page.Gap = 5;

        // TODO: Configurable
        page.Margins.Top = Dimension.FromInches(.22);
        page.Margins.Bottom = Dimension.FromInches(.19);
        page.Margins.Left = Dimension.FromInches(.42);
        page.Margins.Right = Dimension.FromInches(.26);

        IStackContainer headerRow = page.AddStack(Direction.Horizontal);

        BuildHeader(headerRow);

        IStackContainer reportTitleColumn = page.AddStack(Direction.Vertical);
        reportTitleColumn.ElementAlignment = ElementAlignment.Center;

        ITextElement reportTitleText = reportTitleColumn.AddText("VERY IMPORTANT REPORT");
        reportTitleText.FontSize = 10.5;
        reportTitleText.FontStyles = FontStyles.Bold;

        page.AddLine();

        AddReportDetailsSection(page);

        page.AddLine();

        AddResultsOfConversionSection(page);

        page.AddLine();

        AddDisclaimersSection(page);

        page.AddLine();

        ITextElement text = page.AddText("NOT FOR COMMERCIAL USE");
        text.Sizing = ElementSizing.ExpandToFillBounds;
        text.FontStyles = FontStyles.Bold;
        text.FontSize = 30;
        text.StringFormat = StringFormat.TopCenter;
        text.FontColor = Color.FromHex("#bdbdbd");

        AddFooter(page);

        return pdf;
    }

    private static void AddDisclaimersSection(IContainer container)
    {
        IStackContainer disclaimersStack = container.AddStack(Direction.Vertical);
        disclaimersStack.FontSize = 9.5;

        disclaimersStack.AddText("Limitations:").FontStyles = FontStyles.Underline;

        disclaimersStack.AddText(
            "Vituperatoribus persius deserunt condimentum aeque justo consetetur senectus iuvaret voluptaria conubia instructior persius semper usu sapientem quaestio reprimique justo tractatos tractatos platonem penatibus unum pulvinar curabitur habeo vivamus expetendis duis inimicus metus et atqui vim dicat vituperata per labores commodo indoctum dicat agam vulputate dolore saepe tractatos inciderint libris epicuri cu definitiones convallis velit ex praesent elaboraret nisi conceptam cras curabitur netus mel fuisset duo dico consetetur meliore odio signiferumque dicam eu nostra magnis donec luptatum");

        disclaimersStack.AddText("Remarks:").FontStyles = FontStyles.Underline;

        disclaimersStack.AddText(
            "Vituperatoribus persius deserunt condimentum aeque justo consetetur senectus iuvaret voluptaria conubia instructior persius semper usu sapientem quaestio reprimique justo tractatos tractatos platonem penatibus unum pulvinar curabitur habeo vivamus expetendis duis inimicus metus et atqui vim dicat vituperata per labores commodo indoctum dicat agam vulputate dolore saepe tractatos inciderint libris epicuri cu definitiones convallis velit ex praesent elaboraret nisi conceptam cras curabitur netus mel fuisset duo dico consetetur meliore odio signiferumque dicam eu nostra magnis donec luptatum purus nibh noster fabellas omnesque tempus potenti aeque fuisset rutrum fringilla inimicus esse sagittis inciderint duo menandri repudiandae assueverit lacus mel definitiones his dicit constituam homero fastidii");
    }

    private static void AddReportDetailsSection(IPage page)
    {
        IStackContainer reportDetailsRow = page.AddStack(Direction.Horizontal);
        reportDetailsRow.FontSize = 10.5;

        AddReportToAgencyColumn(reportDetailsRow);
        AddDateCaseSourceColumn(reportDetailsRow);
    }

    private static void AddReportToAgencyColumn(IStackContainer reportDetailsRow)
    {
        IStackContainer reportToAgencyColumn = reportDetailsRow.AddStack(Direction.Horizontal);
        reportToAgencyColumn.Sizing = ElementSizing.ExpandToFillBounds;

        BuildReportToAgencyColumn(reportToAgencyColumn);
    }

    private static void AddDateCaseSourceColumn(IStackContainer reportDetailsRow)
    {
        IStackContainer dateCaseSourceColumn = reportDetailsRow.AddStack(Direction.Horizontal);
        dateCaseSourceColumn.Sizing = ElementSizing.ExpandToFillBounds;

        BuildDateCaseSourceColumn(dateCaseSourceColumn);
    }

    private static void BuildDateCaseSourceColumn(IStackContainer dateCaseSourceColumn)
    {
        IStackContainer headerColumn = dateCaseSourceColumn.AddStack(Direction.Vertical);
        headerColumn.ElementAlignment = ElementAlignment.Right;
        headerColumn.Margins.Right = 2;
        headerColumn.FontStyles = FontStyles.Bold | FontStyles.Underline;
        headerColumn.Gap = 1.5;

        headerColumn.AddText("Date of Report:");
        headerColumn.AddText("Case #:");
        headerColumn.AddText("Source:");

        IStackContainer valueColumn = dateCaseSourceColumn.AddStack(Direction.Vertical);
        valueColumn.Margins.Left = 2;
        valueColumn.Gap = 1.5;

        valueColumn.AddText(DateTime.Today.ToShortDateString());
        valueColumn.AddText("123456");
        valueColumn.AddText("Test");
    }

    private static void BuildReportToAgencyColumn(IStackContainer reportToAgencyColumn)
    {
        IStackContainer headerColumn = reportToAgencyColumn.AddStack(Direction.Vertical);
        headerColumn.ElementAlignment = ElementAlignment.Right;
        headerColumn.Margins.Right = 2;
        headerColumn.FontStyles = FontStyles.Bold;
        headerColumn.Gap = 1.5;
        headerColumn.Width = 60;

        headerColumn.AddText("Report To:");
        headerColumn.AddText("Agency:");

        IStackContainer valuesColumn = reportToAgencyColumn.AddStack(Direction.Vertical);
        valuesColumn.Margins.Left = 2;
        valuesColumn.Gap = 1.5;

        valuesColumn.AddText("Test");

        valuesColumn.AddText("Test");
        valuesColumn.AddText("Test");
        valuesColumn.AddText("Test, Test, 12345");
    }

    private static void BuildHeader(IStackContainer headerRow)
    {
        BuildFirstHeaderColumn(headerRow.AddStack(Direction.Vertical));
        BuildMiddleHeaderColumn(headerRow.AddStack(Direction.Vertical));
    }

    private static void BuildFirstHeaderColumn(IStackContainer column)
    {
        column.ElementAlignment = ElementAlignment.Center;
        column.FontSize = 9;

        ITextElement textElement = column.AddText("Test");
        textElement.Margins.Top = 15;

        column.AddText("Important");
    }

    private static void BuildMiddleHeaderColumn(IStackContainer column)
    {
        column.ElementAlignment = ElementAlignment.Center;
        column.Sizing = ElementSizing.ExpandToFillBounds;
        column.FontStyles = FontStyles.Bold;

        ITextElement stateOfCtText = column.AddText("THIS IS THE HEADER");
        stateOfCtText.FontSize = 12;

        IStackContainer departmentStack = column.AddStack(Direction.Vertical);
        departmentStack.ElementAlignment = ElementAlignment.Center;
        departmentStack.FontSize = 9.5;

        departmentStack.AddText("THESE ARE\r\nMULTIPLE LINES of TEXT")
            .StringFormat = StringFormat.Center;

        departmentStack.AddText("THIS IS ANOTHER LINE OF TEXT");

        IStackContainer addressStack = column.AddStack(Direction.Vertical);
        addressStack.ElementAlignment = ElementAlignment.Center;
        addressStack.FontSize = 8.5;

        addressStack.AddText("Salutatus phasellus nihil venenatis");

        addressStack.AddText("commune prodesset elementum cu dictumst");
    }

    private static void AddResultsOfConversionSection(IContainer container)
    {
        IStackContainer resultsOfConversionStack = container.AddStack(Direction.Vertical);
        resultsOfConversionStack.FontSize = 9.5;
        resultsOfConversionStack.LineHeight = 1.5;
        resultsOfConversionStack.Gap = 13;

        AddResultsOfConversionHeaderAndSerumValue(resultsOfConversionStack);

        AddConvertedValue(resultsOfConversionStack);
    }

    private static void AddResultsOfConversionHeaderAndSerumValue(IContainer container)
    {
        IStackContainer resultsOfConversionAndReportSerumStack = container.AddStack(Direction.Vertical);

        ITextElement textElement = resultsOfConversionAndReportSerumStack.AddText("Results of Conversion:");
        textElement.FontStyles = FontStyles.Bold;

        AddReportedSerumValue(resultsOfConversionAndReportSerumStack);
    }

    private static void AddConvertedValue(IContainer container)
    {
        IStackContainer conversionDetailsStack = container.AddStack(Direction.Horizontal);

        conversionDetailsStack.AddText("Salutatus phasellus nihil venenatis volumus ignota: ");

        conversionDetailsStack.AddText("Less than 0.01 gram percent (<0.01 g%).").FontStyles = FontStyles.Bold;
    }

    private static void AddReportedSerumValue(IContainer container)
    {
        IStackContainer reportedValueStack = container.AddStack(Direction.Horizontal);
        reportedValueStack.AddText(" non maecenas lorem leo montes scripta: ");

        reportedValueStack.AddText("10.23456 grams per deciliter (10.23456 g/dL).").FontStyles = FontStyles.Bold;
    }

    private static void AddFooter(IContainer container)
    {
        IStackContainer footerContainer = container.AddStack(Direction.Horizontal);

        AddDataInputBy(footerContainer);
        ITextElement pageText = footerContainer.AddText("Page 1 of 1");
        pageText.Sizing = ElementSizing.ExpandToFillBounds;
        pageText.StringFormat = StringFormat.TopRight;
    }

    private static void AddDataInputBy(IContainer container)
    {
        IStackContainer dataInputByStack = container.AddStack(Direction.Vertical);
        dataInputByStack.FontSize = 10.5;

        ITextElement dataInputBy = dataInputByStack.AddText("Data Input by:");
        dataInputBy.FontStyles = FontStyles.Bold | FontStyles.Underline;

        dataInputByStack.AddText("Test");

        dataInputByStack.AddText("Test 123");
    }
}