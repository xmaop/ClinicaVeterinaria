// Type: RazorEngine.Compilation.RazorDynamicObject
// Assembly: RazorEngine, Version=2.1.4113.149, Culture=neutral, PublicKeyToken=1f722ed313f51831
// MVID: A30766E5-F1D4-4896-87D6-1F301365FAC1
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorEngine.dll

using System;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;

namespace PetCenter_GCP.RazorEngine.Compilation
{
    internal class RazorDynamicObject : DynamicObject
    {
        public object Model { get; set; }

        [DebuggerStepThrough]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            RazorDynamicObject razorDynamicObject = this.Model as RazorDynamicObject;
            if (razorDynamicObject != null)
                return razorDynamicObject.TryGetMember(binder, out result);
            PropertyInfo property = this.Model.GetType().GetProperty(binder.Name);
            if (property == (PropertyInfo)null)
            {
                result = (object)null;
                return false;
            }
            else
            {
                object obj1 = property.GetValue(this.Model, (object[])null);
                if (obj1 == null)
                {
                    result = obj1;
                    return true;
                }
                else
                {
                    Type type = obj1.GetType();
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    object local = result;
                    object obj2;
                    if (!CompilerServices.IsAnonymousType(type))
                        obj2 = obj1;
                    else
                        obj2 = (object)new RazorDynamicObject()
                        {
                            Model = obj1
                        };
                    // ISSUE: explicit reference operation
                    local = obj2;
                    return true;
                }
            }
        }
    }
}
