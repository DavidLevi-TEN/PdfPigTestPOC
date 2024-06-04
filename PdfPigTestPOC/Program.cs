using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

//PdfDocument implements IDisposable, so it has to be in a using statement

var client = new HttpClient();
var response = await client.GetAsync("https://clickdimensions.com/links/TestPDFfile.pdf");

var message = await response.Content.ReadAsStreamAsync();

using (PdfDocument document = PdfDocument.Open(message))
{
    //An example from a site listing the best libraries for extracting text from PDFs,
    //which has a link to the Github page for this library
    foreach (var page in document.GetPages())
    {
        //This example offers three ways to extract text from a page

        //1. Extract based on the order of content in the document.
        //This method includes newlines and spaces, if you're worried about formatting.
        var text = ContentOrderTextExtractor.GetText(page);

        //2. Group letters into words and extract based on that.
        //After seeing the results, I have no idea why you would do this when you can just get the raw text.
        var otherText = string.Join(" ", page.GetWords());

        //Or 3. Just extract the raw text of the page's content stream.
        var rawText = page.Text;

        Console.WriteLine(text);
    }
}

//Everything below this is examples from the library's Github page

/*It can also access document metadata

PdfDocument document = PdfDocument.Open(fileName);

// The name of the program used to convert this document to PDF.
string producer = document.Information.Producer;

// The title given to the document
string title = document.Information.Title;
// etc...


//Apparently this package can be used to open encrypted documents.
//I don't know when we would need to do this, but if this library is ever needed for something like that, well.
using (PdfDocument document = PdfDocument.Open(@"C:\file.pdf", new ParsingOptions
{
    Passwords = new List<string> { "One", "Two" }
}))*/