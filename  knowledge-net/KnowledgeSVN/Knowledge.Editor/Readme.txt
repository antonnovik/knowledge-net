Some notes

*** Setup ***

- perhaps, we could use ".vscontent" file to install plugin
  instead of setup* projects
  
- we need to make possible install Knowledge.NET if Aspect.NET
  is installed

- it would be useful useful activate logging somehow

- http://www.knowdotnet.com/articles/addininstallation.html
  VS2005 Add-In: The new methodology replaces registration
  with an XML file that describes the original add-in manager
  fields and points to the assembly for the add-in.  

- The new technology doesn't work sometimes
  I couldn't register any add-in on WinXP Rus HomeEdition
  though out-of-date regisration (via registry) works just fine
  In that case, using another OS seems as only workaround.

- For debugging purpose path to add-in's dll is hardcoded for a while.

- If you encounted error #80070002 then most likely building of add-in's
  solution failed. Please, check it.

*** Logging ***

- To enable addin-logging you could "merge"
"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\"devenv.exe.config 
"C:\export\Knowledge.NET\ws\20060418\Knowledge.NET.config 

- A way to debug VS is pass "/log" to dteenv.exe
Look after to that at 
"C:\Documents and Settings\Dmitry\Application Data\Microsoft\VisualStudio\8.0\"ActivityLog.xml 

*** C# ***

This is a stupid question...
But why invokation setter of variable inside ctor
will throw StackOverflowException?

Useful tip on VS syntax highlighting
http://codebetter.com/blogs/darrell.norton/archive/2004/04/21/11837.aspx
we need investigate possibility of highlighting of new keywords ...

*** Editor ***

TODO:

- clean up

- change the icon of main window

- opened event is not coming if open vs by pressing on sln file

- go through the doc