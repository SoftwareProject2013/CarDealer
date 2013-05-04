using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializable
{

    /// <summary>
    /// The files are saved and loaded by default from the directory: C:"path"\CarDealer\WpfApplicationCarDealer\bin\Debug
    /// The files are in the GUI project because they are called from the GUI
    /// </summary>
    public class Serialization
    {

        /// <summary>
        /// Allows to save an object. We have to give an object and a name file
        /// </summary>
        /// <param name="toSave"></param>
        /// <param name="path"></param>
        public void Enregistrer(object toSave, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream flux = null;
            try
            {
                flux = new FileStream(path, FileMode.Create, FileAccess.Write);
                formatter.Serialize(flux, toSave);
                flux.Flush();
            }
            catch (Exception e1)
            {
                throw e1;
            }
            finally
            {
                if (flux != null)
                    flux.Close();
            }
        }


        /// <summary>
        /// Allows to load the file and take the object saved in a file. We have to give the name of the file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Charger<T>(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream flux = null;
            try
            {
                flux = new FileStream(path, FileMode.Open, FileAccess.Read);

                return (T)formatter.Deserialize(flux);
            }
            catch (Exception e1)
            {

                throw e1;
            }
            finally
            {
                if (flux != null)
                    flux.Close();
            }

        }





    }
}
