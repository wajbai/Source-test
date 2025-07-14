/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Bosco.Utility.CommonMemberSet;

namespace Bosco.Utility
{
    public class CommonMember
    {
        private NumberSetMember numberSet = null;
        private DateSetMember dateSet = null;
        private ArraySetMember arraySet = null;
        private StringSetMember stringSet = null;
        private ListSetMember listSet = null;
        private ComboSetMember comboSet = null;
        private GridSetMember gridSet = null;
        private FileSetMember fileSet = null;
        private EnumSetMember enumSet = null;

        #region Member Group

        public NumberSetMember NumberSet
        {
            get { if (numberSet == null) numberSet = new NumberSetMember(); return numberSet; }
        }

        public DateSetMember DateSet
        {
            get { if (dateSet == null) dateSet = new DateSetMember(); return dateSet; }
        }

        public ArraySetMember ArraySet
        {
            get { if (arraySet == null) arraySet = new ArraySetMember(); return arraySet; }
        }

        public StringSetMember StringSet
        {
            get { if (stringSet == null) stringSet = new StringSetMember(); return stringSet; }
        }

        public ListSetMember ListSet
        {
            get { if (listSet == null) listSet = new ListSetMember(); return listSet; }
        }

        public ComboSetMember ComboSet
        {
            get { if (comboSet == null) comboSet = new ComboSetMember(); return comboSet; }
        }

        public GridSetMember GridSet
        {
            get { if (gridSet == null) gridSet = new GridSetMember(); return gridSet; }
        }

        public FileSetMember FileSet
        {
            get { if (fileSet == null) fileSet = new FileSetMember(); return fileSet; }
        }

        public EnumSetMember EnumSet
        {
            get { if (enumSet == null) enumSet = new EnumSetMember(); return enumSet; }
        }

        #endregion

        public object GetDynamicInstance(string instanceType, object[] args)
        {
            Type type = Type.GetType(instanceType, false, true);
            object instance = null;

            if (type != null)
            {
                try
                {
                    if (args != null)
                    {
                        instance = System.Activator.CreateInstance(type, args);
                    }
                    else
                    {
                        instance = System.Activator.CreateInstance(type);
                    }
                }
                catch (Exception e)
                {
                    throw new ExceptionHandler(e, true);
                }
            }

            return instance;
        }
    }
}
