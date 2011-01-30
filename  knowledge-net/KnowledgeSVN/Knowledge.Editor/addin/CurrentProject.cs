using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace Knowledge.Editor.AddIn
{
	public enum Lang { CSharp, VbNet, Unknown, NULL};

	public class CurrentProject
	{
		private Project project;

		public Project Project
		{
			get
			{
				return project;
			}
			set
			{
				project = Project;
			}
		}

		public Lang Language
		{
			get
			{
				if (project == null) return Lang.NULL;
				if (project.FileName.EndsWith("csproj"))
					return Lang.CSharp;
				else if (project.FileName.EndsWith("vbproj"))
					return Lang.VbNet;
				else
					return Lang.Unknown;
			}
		}

		public CurrentProject(Project _project)
		{
			project = _project;
		}

		/// <summary>
		/// Checks whether the project is supported by ANF (C# or VB.NET)
		/// </summary>
		public static bool IsSupportedProjectType(Project prj)
		{
			if (prj == null) return false;

			if (prj.FileName.EndsWith("csproj"))
				return true;
			else if (prj.FileName.EndsWith("vbproj"))
				return true;
			else
				return false;
		}

		public static Project GetSelectedProject(DTE2 applicationObject)
		{
			EnvDTE.Project project = null;
			//project = (Project)(((Array)(app.ActiveSolutionProjects)).GetValue(0));
			Array projects = (Array)(applicationObject.ActiveSolutionProjects);
			if (projects.Length > 0)
			{
				/*
				try
				{
					EnvDTE.SolutionClass sc = (EnvDTE.SolutionClass)projects.GetValue(0);
					Project p = sc.Projects.Item(0);
				}
				catch (Exception e1) { }
				 * */

				project = (EnvDTE.Project)(projects.GetValue(0));

			}
			return project;
		}
	}
}
