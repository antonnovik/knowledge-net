/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using Knowledge.KIF.Converter.model;

namespace Knowledge.KIF.Converter.converters {
    public interface IModelBuilder {

        IModel getModel();
        
        void createClassFrame(DataFrame frame);
        void createInstanceFrame(DataFrame frame);
        void createConcept();//TODO:
        void createProperty();//TODO:
        void setDirector(Converter converter);//TODO:
    }
}