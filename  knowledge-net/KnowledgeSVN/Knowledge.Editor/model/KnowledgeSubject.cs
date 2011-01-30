using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using log4net;

using Knowledge.Editor.View;

/*
 * This class is part of implementation of Observer
 * pattern. It holds model's data. Also, it updates
 * observers if notification has been sent by client.
 */
namespace Knowledge.Editor.Model
{
    public interface ISubject 
    {
        void attach(IObserver observer);
        void detach(IObserver observer);
        void notify();
        // TODO: get rid of
        void convert();
    }

    public class KnowledgeModel : ISubject
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeModel));

        // TODO: make private
        public KnowledgeAdapter data;

        public KnowledgeModel() {
            log.Debug(" model ctor ... ");
        }        

        public void buildModel(Knowledge.Editor.AddIn.Environment environment) {

            log.Debug(" building model ... ");

            data = new KnowledgeAdapter();
            data.parse(environment);

            log.Debug(" model built. ");
        }

        // TODO: make List<IObserver>
        IList observers = new ArrayList();

        public void attach(IObserver observer) {
            log.Debug(" attach - "+observer);
            observers.Add(observer);
        }

        public void detach(IObserver observer) {
            log.Debug(" detached - " + observer);
            observers.Remove(observer);
        }

        public void notify() {
            log.Debug(" notifying observers - "+observers.Count);

            if (data == null) {
                log.Debug(" data is null ");
                return;
            }

            if (data.isEmpty) {
                log.Debug(" model is empty ");
                return;
            }

            foreach(Object observer in observers){
                log.Debug(" notifing - "+observer+" ... ");

                ((IObserver)observer).update(this);
            }
        }

        public void convert() { 
            log.Debug(" notifying observers - "+observers.Count);

            if (data == null) {
                log.Debug(" data is null ");
                return;
            }

            if (data.isEmpty) {
                log.Debug(" model is empty ");
                return;
            }

            foreach(Object observer in observers){
                if (observer.GetType() == typeof(KnowledgeCsView))
                {
                    log.Debug(" convertation - "+observer+" ... ");
                    ((IObserver)observer).update(this);
                }
            }
        }
    }
}