using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.Skins;
using System.Drawing;

namespace Bosco.Utility.Controls {
    [ToolboxItem(true)]
    public class MyToggleSwitch : ToggleSwitch {
        static MyToggleSwitch() { RepositoryItemMyToggleSwitch.RegisterMyToggleSwitch(); }

        public MyToggleSwitch() {
            
        }

        public override string EditorTypeName {
            get { return RepositoryItemMyToggleSwitch.MyToggleSwitchName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMyToggleSwitch Properties {
            get { return base.Properties as RepositoryItemMyToggleSwitch; }
        }
    }

    [UserRepositoryItem("RegisterMyToggleSwitch")]
    public class RepositoryItemMyToggleSwitch : RepositoryItemToggleSwitch
    {
        static RepositoryItemMyToggleSwitch() { RegisterMyToggleSwitch(); }

        public RepositoryItemMyToggleSwitch()
        {
            useDefaultMode = true;
        }

        public const string MyToggleSwitchName = "CustomEdit";

        public override string EditorTypeName { get { return MyToggleSwitchName; } }

        public static void RegisterMyToggleSwitch()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(MyToggleSwitchName, typeof(MyToggleSwitch), typeof(RepositoryItemMyToggleSwitch), typeof(MyToggleSwitchViewInfo), new MyToggleSwitchPainter(), true, null));
        }

        private bool useDefaultMode;

        public bool UseDefaultMode
        {
            get { return useDefaultMode; }
            set
            {
                if (useDefaultMode != value)
                {
                    useDefaultMode = value;
                    OnPropertiesChanged();
                }
            }
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMyToggleSwitch source = item as RepositoryItemMyToggleSwitch;
                if (source == null) return;
                useDefaultMode = source.UseDefaultMode;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    public class MyToggleSwitchPainter : ToggleSwitchPainter
    {

    }

    public class MyToggleSwitchViewInfo : ToggleSwitchViewInfo
    {
        public MyToggleSwitchViewInfo(RepositoryItem item) : base(item) { }

        protected override BaseCheckObjectPainter CreateCheckPainter()
        {
            return new MySkinToggleObjectPainter(LookAndFeel);
        }
    }

    public class MySkinToggleObjectPainter : SkinToggleObjectPainter
    {
        ISkinProvider provider;
        public MySkinToggleObjectPainter(ISkinProvider provider)
            : base(provider)
        {
            this.provider = provider;
        }

        protected override double PercentRatio { get { return 0.75; } }

        /*protected override void DrawToggleSwitchThumb(ToggleObjectInfoArgs args)
        {
            SkinElementInfo info = GetToggleSwitchThumbSkinElementInfo(args);
            info.Bounds = args.Bounds;
            info.Bounds = new Rectangle(info.Bounds.Location, new Size(info.Bounds.Width, info.Bounds.Height + 3));
            if (args.IsOn)
            {
                info.OffsetContent(0, -3);
            }
            else
            {
                info.OffsetContent(-4, -3);
            }
            SkinElementPainter.Default.DrawObject(info);
        }*/
    }
}
