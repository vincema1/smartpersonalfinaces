using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.DATA
{
    public abstract class ARepository
    {
    #region POCO methods
        public static List<P> FromListEntityToListPOCO<P, E>(List<E> entity)
        {
            List<P> listP = new List<P>();

            foreach (E item in entity)
            {
                P tmp = FromEntityToPOCO<P, E>(item);
                listP.Add(tmp);
            }

            return listP;
        }
        public static P FromEntityToPOCO<P, E>(E entity)
        {

            P tmp = (P)Activator.CreateInstance(typeof(P));

            Type p = typeof(P);
            Type e = typeof(E);

            foreach (PropertyInfo propT in p.GetProperties())
            {
                foreach (PropertyInfo propE in e.GetProperties())
                {
                    if (propT.Name == propE.Name)
                    {
                        object value = GetPropertyValue(entity, propE.Name);
                        SetPropertyValueWithCast(propT, tmp, value);
                        break;
                    }

                }
            }

            return tmp;
        }

        private static void SetPropertyValueWithCast(PropertyInfo prop,
                                     object objectInstance,
                                     object value)
        {
            Type tl = prop.PropertyType;
            dynamic changedObj;

            //You have to use Nullable.GetUnderlyingType to get underlying type of Nullable.
            if (tl.IsGenericType && tl.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                tl = Nullable.GetUnderlyingType(tl);

            changedObj = Convert.ChangeType(value, tl);
            prop.SetValue(objectInstance, changedObj);
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperties()
               .Single(pi => pi.Name == propertyName)
               .GetValue(obj, null);
        }

#endregion
    }
}
