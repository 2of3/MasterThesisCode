﻿using System;
using System.Xml.Serialization;

namespace FuseeAuthoringTools
{
    public static class GlobalValues
    {
        public const String PROJECTFOLDER = "/projects";
        public const String COMPILEINCLUDESTART = "     <Compile Include=\"Main.cs\" />";
    }

    /// <summary>
    /// This enum is for returning more readable values in functions than just boolean.
    /// </summary>
    public enum ToolState {
        OK = 0,
        ERROR = 1,
        WARNING = 2,
    }

    public enum ProjectState
    {
        Clean = 0, // means open, too
        Dirty = 1,
        Corrupt = 2,
        Closed = 3
    }

    /// <summary>
    /// A container for some information about a project.
    /// Keeps the project state and also the paths.
    /// </summary>
    public struct EngineProject
    {
        [XmlElement("PathToSolutionFolder")]
        public String sysPath; // path to solution dir

        [XmlElement("PathToProjectFolder")]
        public String projPath; // sysPath + /projects/pName/

        [XmlElement("ProjectName")]
        public String nameofCSPROJ; // name.csproj

        [XmlElement("PathToCSProj")]
        public String pathToCSPROJ; // path to csproj.

        [XmlElement("CurrentProjectState")]
        public ProjectState projectState;
    }

    interface IFuseeAuthoringTools
    {
        Boolean CreateProject(String pName, String pPath);
        Boolean SaveProject();
        Boolean OpenProject(String pName, String pPath);
        Boolean UpdateProject();

        Boolean CreateNewClass(String pName, String pPath);
        Boolean CreateNewFile();

        Boolean BuildProject();

        EngineProject GetEngineProject();
        int GetEngineProjectState();
    }
}