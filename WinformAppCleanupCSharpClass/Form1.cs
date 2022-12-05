using System.Configuration;
using System.Diagnostics;
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

    private static string StripComments(string input)
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

    private void btnProcessFolder_Click(object sender, EventArgs e)
    {
        var folder = txtInputFolder.Text;
        folder = @"C:\code\cyberSourceAll\cybersource-rest-client-dotnetstandard\cybersource-rest-client-netstandard\cybersource-rest-client-netstandard\Model";
        folder = @"C:\devMe\cyberSourceAll\cybersource-rest-client-dotnetstandard\cybersource-rest-client-netstandard\cybersource-rest-client-netstandard\Model";
        folder = @"C:\devMe\cyberSourceAll\cybersource-rest-client-dotnetstandard\cybersource-rest-client-netstandard\cybersource-rest-client-netstandardMe\Model";
        var di = new DirectoryInfo(folder);
        if (!di.Exists)
            return;

        var ticks = DateTime.Now.Ticks;


        //var ns = DateTime.Now.ToString("yyyyMMddfff");

        var outDirStr = $@"c:\temp\cybersource-rest-client-netstandard\Model{ticks}";
        DirectoryInfo outFolder = Directory.CreateDirectory(outDirStr);

        FileInfo[] files = di.GetFiles("*.cs");
        
        FileInfo firstF = files[0];
        //ProcessInputFile.ProcessFile(firstF, outFolder);
        foreach (var fileInfo in files)
        {
            ProcessInputFile.ProcessFile(fileInfo, outFolder);  
        }

        var projFileContent = 
@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net7.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""BouncyCastle.NetCore"" Version=""1.9.0"" />
    <PackageReference Include=""jose-jwt"" Version=""4.0.1"" />
    <PackageReference Include=""Newtonsoft.Json"" Version=""13.0.1"" />
  </ItemGroup>


</Project>
";
        var csProjFilePath = Path.Combine(outDirStr, $"{DateTime.Now.Ticks}Models.csproj");
        File.WriteAllText(csProjFilePath, projFileContent);
        
        var entryMethod = """
            internal class Entry
            {
                public static void Main(string[] args)
                {

                }
            }
            """;
        var mainPath = Path.Combine(outDirStr, $"Entry.cs");
        File.WriteAllText(mainPath, entryMethod);



        Process.Start("explorer.exe", outFolder.FullName);
    }

    private void btnRename_Click(object sender, EventArgs e)
    {
        var folder = @"C:\temp\cybersource-rest-client-netstandard\Model638058469904799913\Response";
        var di = new DirectoryInfo(folder);
        var files = di.GetFiles();
        foreach (FileInfo f in files)
        {
            File.Move(f.FullName, f.FullName.Replace("PtsV2PaymentsPost201Response", ""));
            f.Delete();
        }
        //foreach (FileInfo fi in files)
        //{
        //    var existingFileName = fi.FullName;
        //    var prefix = "Ptsv2payments";
            
        //    if (existingFileName.StartsWith(prefix))
        //    {
        //        var newFileName = existingFileName.Replace(prefix, "");
        //        File.Move(existingFileName, newFileName); // Rename the oldFileName into newFileName
        //        File.Delete(newFileName); // Delete the existing file if exists

        //    }
        //}
    }
}

public static class ProcessInputFile
{
    public static void ProcessFile(FileInfo inputFile, DirectoryInfo outputDirectoryInfo)
    {
        var fullName = inputFile.FullName;
        var allText = File.ReadAllText(fullName);
        //var allText = File.ReadAllText(fullName);

        allText = StripComments(allText);

        var isMatch = false;
        var removeEmptyLines = @"^(\s)*$\n";
        var removeEmptyLinesRegex = new Regex(removeEmptyLines, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        isMatch = removeEmptyLinesRegex.IsMatch(allText);
        allText = removeEmptyLinesRegex.Replace(allText, "");

        var outputDir = outputDirectoryInfo;
        var outFileName = inputFile.Name;
        //var outFileNameTemp = $"{inputFile.Name}tmp.cs";
        var outFileNameTemp = Path.Combine(outputDir.FullName, outFileName);

        File.WriteAllText(outFileNameTemp, allText);

        //var regexCtorString = @"^\s*public " + className + @".[\s*.*\{\w=\(\),;}]*";
        //var removeCtorRegex = new Regex(regexCtorString, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //isMatch = removeCtorRegex.IsMatch(allText);

    }

    private static string StripComments(string input)
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