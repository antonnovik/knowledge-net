using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using EnvDTE;

using log4net;

using Knowledge.Editor.Model;
using Knowledge.Editor.View;
using Knowledge.Editor.AddIn;

namespace Knowledge.Editor.Controller
{
    public class KnowledgeController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeController));

        protected static KnowledgeController _instance = null;

        public static KnowledgeController instance(Environment environment,
                                                   Control control) { 
            if (_instance == null) {
                _instance = new KnowledgeController(environment, control);
            }
            log.Debug(" return controller: "+_instance);
            return _instance;
        }

        // TODO: get rid of
        public static KnowledgeController instance() {
            if (_instance == null) {
                log.Debug(" controller is null ");
            }
            return _instance;
        }

        ISubject subject;
        IObserver textcs;
        IObserver textnet;
        IObserver tree;

        private KnowledgeController(Environment environment, Control control) {

            log.Debug(" controller creation ... ");

            this.environment = environment;
            this.control = control;

            subject = new KnowledgeModel();
            textcs = new KnowledgeCsView(this, (KnowledgeModel)subject);
            textnet = new KnowledgeNetView(this, (KnowledgeModel)subject);

            // TODO: using this inside ctor is not good
            tree = new KnowledgeTreeView(this, (KnowledgeModel)subject,
                                         (KnowledgeControl)control);

            //subject.attach(textcs);
            subject.attach(textnet);
            subject.attach(tree);

            registerEvents();

            log.Debug(" controller created. ");
        }

        // TODO: make private
        public Environment environment;
        public Control control;

        private EnvDTE.SolutionEvents solutionEvents;

        private void registerEvents() {
            solutionEvents = (SolutionEvents)environment.ApplicationObject.Events.SolutionEvents;
            solutionEvents.Opened += new _dispSolutionEvents_OpenedEventHandler(this.opened);

            /*
            solutionEvents.AfterClosing += new _dispSolutionEvents_AfterClosingEventHandler(this.AfterClosing);
            solutionEvents.ProjectAdded += new _dispSolutionEvents_ProjectAddedEventHandler(this.ProjectAdded);
            solutionEvents.QueryCloseSolution += new _dispSolutionEvents_QueryCloseSolutionEventHandler(this.QueryCloseSolution);
            solutionEvents.AfterClosing += new _dispSolutionEvents_AfterClosingEventHandler(this.AfterClosing);
            solutionEvents.BeforeClosing += new _dispSolutionEvents_BeforeClosingEventHandler(this.BeforeClosing);
            solutionEvents.Opened += new _dispSolutionEvents_OpenedEventHandler(this.Opened);
            solutionEvents.ProjectAdded += new _dispSolutionEvents_ProjectAddedEventHandler(this.ProjectAdded);
            solutionEvents.ProjectRemoved += new _dispSolutionEvents_ProjectRemovedEventHandler(this.ProjectRemoved);
            solutionEvents.ProjectRenamed += new _dispSolutionEvents_ProjectRenamedEventHandler(this.ProjectRenamed);
            solutionEvents.QueryCloseSolution += new _dispSolutionEvents_QueryCloseSolutionEventHandler(this.QueryCloseSolution);
            solutionEvents.Renamed += new _dispSolutionEvents_RenamedEventHandler(this.Renamed);
            */
        }

        public void opened() {

            log.Debug(" opening sln ... ");

            try
            {
                ((KnowledgeModel)subject).buildModel(environment);
                subject.notify();
            }catch(System.Exception e){
                log.Debug(e);
            }

            log.Debug(" sln opened. ");

        }

        public void convert() {
            log.Debug(" convertation ... ");

            subject.attach(textcs);
            subject.convert();

            log.Debug(" convertation.");
        }

        // TODO:
        // implement detach / -=
    }
}
