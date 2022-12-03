using System.Text.RegularExpressions;

namespace WinformAppCleanupCSharpClass;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnProcess_Click(object sender, EventArgs e)
    {
        var input = txtInput.Text;

        //var output = ProcessText.Process(input);
        var output = StripComments(input);

        txtOutput.Text = output;
    }

    private string DoStuff(string input)
    {
        var output = txtOutput.Text;
        return output;
    }

    //private string StripCtor(string input)
    //{
    //    Regex r = new Regex("public partial class CreateSearchRequest) ")
    //}

    private string StripComments(string input)
    {
        var blockComments = @"/\*(.*?)\*/";
        var lineComments = @"//(.*?)\r?\n";
        var strings = @"""((\\[^\n]|[^""\n])*)""";
        var verbatimStrings = @"@(""[^""]*"")+";
        var noComments = Regex.Replace(input,
            blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
            me =>
            {
                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                    return me.Value.StartsWith("//") ? Environment.NewLine : "";
                // Keep the literal strings
                return me.Value;
            },
            RegexOptions.Singleline);
        return noComments;
    }
}