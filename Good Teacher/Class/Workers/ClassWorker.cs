using System;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Media;

namespace Good_Teacher.Class
{
    class ClassWorker
    {

        public static object Clone(Object obj)
        {
            object new_obj = Activator.CreateInstance(obj.GetType());

            foreach (FieldInfo pi in obj.GetType().GetFields(BindingFlags.NonPublic |
                         BindingFlags.Instance | BindingFlags.Public))
            {
                //Debug.WriteLine(pi.Name + " - " + pi.GetValue(obj));

                pi.SetValue(new_obj, pi.GetValue(obj));
            }
            return new_obj;
        }


        public static Color ColorFromBrush(Brush brush)
        {
            if (brush != null)
            {
                if (brush is SolidColorBrush)
                {
                    return ((SolidColorBrush)brush).Color;
                }
                else
                {
                    return Colors.Transparent;
                }

            } else
                return Colors.Transparent;
        }


        public static object Copy(object copyobject)
        {
            return XamlReader.Parse(XamlWriter.Save(copyobject));
        }


        public static object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();

            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);

            foreach (PropertyInfo pi in properties)
            {

                if (pi.CanWrite)
                {
                    //Debug.WriteLine("*** "+pi + "   Name: " + pi.Name + "   Ptype: " + pi.PropertyType + "  REFLECTTYPE: " + pi.ReflectedType + "   Module: " + pi.Module + "    MemberType: " + pi.MemberType + "   DeclrType: " + pi.DeclaringType);
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p;
        }


    }
}
