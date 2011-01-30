/*
 * Created by: M. Sigalin
 * Created: Tuesday, November 07, 2006
 */

namespace Knowledge.KIF.Converter.converters {
    public interface ModelBuildListener {
        void buildStarted(int count);
        void buildFinished();
        void buildItem(ModelBuildEvent buildEvent);
    }
}