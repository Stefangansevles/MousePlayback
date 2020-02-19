using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MousePlayback
{
    public class IO
    {
        public static string ApplicationVersion
        {
            get
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    return fvi.FileVersion;
                }
                catch (FileNotFoundException) //For some reason this can throw a FileNotFoundException on RemindMe.exe , let's try the code below
                {
                    try
                    {                        
                        Assembly asm = Assembly.GetExecutingAssembly();
                        AssemblyName asmName = asm.GetName();
                        string versionString = asmName.Version.ToString();

                        if (versionString[versionString.Length - 2] == '.' && versionString[versionString.Length - 1] == '0')
                            versionString = versionString.Substring(0, versionString.Length - 2);

                        return versionString;
                    }
                    catch { return "1.0"; } //Failed AGAIN ? Welp
                }
                catch { return "1.0"; }
            }
        }

        /// <summary>
        /// Serializes the action objects to a file located at the given path
        /// </summary>
        /// <param name="actions">The list of actions you want serialized</param>
        /// <param name="pathToFile">The path to the file that will contain the serialized action objects</param>
        /// <returns>True if succesfull, false if not</returns>
        public static bool Serialize(List<Action> actions, string pathToFile)
        {            
            // Create a hashtable of values that will eventually be serialized.
            Hashtable hashActions = new Hashtable();

            foreach (Action a in actions)
            {                
                hashActions.Add(a.ID, a);
            }
            
            // To serialize the hashtable and its key/value pairs,  
            // you must first open a stream for writing. 
            // In this case, use a file stream.
            FileStream fs = new FileStream(pathToFile, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            
            try
            {                
                formatter.Serialize(fs, hashActions);                
            }
            catch (SerializationException ex)
            {
                ErrorPopup p = new ErrorPopup(ex);
                p.Show();
                return false;
            }
            finally
            {
                fs.Close();
            }
            
            return true;
        }



        /// <summary>
        /// De-Serializes the provided .remindme file located at the given path into a List of Reminder objects
        /// </summary>
        /// <param name="pathToFile">The path to the file that contains the serialized reminder objects</param>
        /// <returns>A list of reminder objects from the given .remindme file</returns>
        public static List<Action> Deserialize(string pathToFile)
        {
            List<Action> toReturnList = new List<Action>();

            // Declare the hashtable reference.
            Hashtable hashAction = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                hashAction = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException)
            {
                return null;
            }
            finally
            {
                fs.Close();
            }

            foreach (DictionaryEntry de in hashAction)
                toReturnList.Add((Action)de.Value);


            return toReturnList;
        }
    }
}
