﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace FuseeProjectGenerator.Templates
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ProjFile : ProjFileBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""4.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <PropertyGroup>
    <SolutionDir Condition=""$(SolutionDir) == '' Or $(SolutionDir) == '*Undefined*'"">$(ProjectDir)../../../../</SolutionDir>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">x86</Platform>
    <ProductVersion>8.0.30703</ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{55C8E245-79A5-4E23-838F-BA959B892197}</ProjectGuid>
    <OutputType>WinExe</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>Examples.");
            
            #line 13 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_name));
            
            #line default
            #line hidden
            this.Write("</RootNamespace>\r\n    <AssemblyName>Examples.");
            
            #line 14 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_name));
            
            #line default
            #line hidden
            this.Write(@"</AssemblyName>
    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <UseVSHostingProcess>false</UseVSHostingProcess>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|x86' "">
    <PlatformTarget>x86</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>..\..\..\..\Bin\Debug\Examples\");
            
            #line 24 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_name));
            
            #line default
            #line hidden
            this.Write(@"\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|x86' "">
    <PlatformTarget>x86</PlatformTarget>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>..\..\..\..\Bin\Release\Examples\");
            
            #line 33 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_name));
            
            #line default
            #line hidden
            this.Write(@"\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup>
    <RunPostBuildEvent>OnBuildSuccess</RunPostBuildEvent>
  </PropertyGroup>
  <PropertyGroup Condition=""'$(Configuration)|$(Platform)' == 'Debug+Web|x86'"">
    <PlatformTarget>x86</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>..\..\..\..\Bin\Debug\Examples\");
            
            #line 46 "C:\Users\dominik\Development\Fusee\src\Uniplug\Cinema4D\fuProjectGen\Templates\ProjFile.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(_name));
            
            #line default
            #line hidden
            this.Write("\\</OutputPath>\r\n    <DefineConstants>DEBUG;TRACE</DefineConstants>\r\n    <ErrorRep" +
                    "ort>prompt</ErrorReport>\r\n    <WarningLevel>4</WarningLevel>\r\n  </PropertyGroup>" +
                    "\r\n  <ItemGroup>\r\n    <Compile Include=\"Main.cs\" />\r\n    <Compile Include=\"Proper" +
                    "ties\\AssemblyInfo.cs\" />\r\n  </ItemGroup>\r\n  <ItemGroup>\r\n    <Folder Include=\"As" +
                    "sets\\\" />\r\n  </ItemGroup>\r\n  <ItemGroup>\r\n    <ProjectReference Include=\"$(Solut" +
                    "ionDir)src\\Engine\\Common\\Common.csproj\">\r\n      <Project>{DFD770F6-4222-4255-AAE" +
                    "0-DCACDC7B21EB}</Project>\r\n      <Name>Common</Name>\r\n    </ProjectReference>\r\n " +
                    "   <ProjectReference Include=\"$(SolutionDir)src\\Engine\\Core\\Core.csproj\">\r\n     " +
                    " <Project>{1228EB3F-8BCC-453F-8A2E-D9246495A118}</Project>\r\n      <Name>Core</Na" +
                    "me>\r\n    </ProjectReference>\r\n    <ProjectReference Include=\"$(SolutionDir)src\\M" +
                    "ath\\Core\\Math.Core.csproj\">\r\n      <Project>{E95FA1C8-6491-4B4B-BBE1-EDA6B16B2C6" +
                    "A}</Project>\r\n      <Name>Math.Core</Name>\r\n    </ProjectReference>\r\n  </ItemGro" +
                    "up>\r\n  <ItemGroup>\r\n    <Reference Include=\"System\" />\r\n  </ItemGroup>\r\n  <Impor" +
                    "t Project=\"$(MSBuildToolsPath)/Microsoft.CSharp.targets\" />\r\n  <PropertyGroup>\r\n" +
                    "    <PostBuildEvent>\r\n    </PostBuildEvent>\r\n  </PropertyGroup>\r\n  <Target Name=" +
                    "\"Clean\">\r\n    <Exec Condition=\" \'$(OS)\' == \'Windows_NT\' And Exists(\'$(OutputPath" +
                    ")\') \" Command=\'rd /s /q \"$(OutputPath)\"\' />\r\n    <Exec Condition=\" \'$(OS)\' != \'W" +
                    "indows_NT\' And Exists(\'$(OutputPath)\') \" Command=\'rm \"$(OutputPath)\" -r -f\' />\r\n" +
                    "  </Target>\r\n  <Import Project=\"$(SolutionDir)Fusee_Windows.targets\" Condition=\"" +
                    "\'$(PostBuildEvent)\' == \'\' And \'$(OS)\' == \'Windows_NT\' \" />\r\n  <Import Project=\"$" +
                    "(SolutionDir)Fusee_Linux.targets\" Condition=\"\'$(PostBuildEvent)\' == \'\' And \'$(OS" +
                    ")\' != \'Windows_NT\' \" />\r\n</Project>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class ProjFileBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
