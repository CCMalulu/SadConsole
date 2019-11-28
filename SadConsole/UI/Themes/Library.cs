﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SadConsole.UI.Controls;

namespace SadConsole.UI.Themes
{
    /// <summary>
    /// The library of themes. Holds the themes of all controls.
    /// </summary>
    [DataContract]
    public class Library
    {
        private static Library _libraryInstance;

        private Dictionary<System.Type, ThemeBase> _controlThemes;

        /// <summary>
        /// If a control does not specify its own theme, the theme from this property will be used.
        /// </summary>
        public static Library Default
        {
            get
            {
                if (_libraryInstance != null) return _libraryInstance;

                _libraryInstance = new Library();
                _libraryInstance.Init();

                return _libraryInstance;
            }
            set
            {
                if (null == value)
                {
                    _libraryInstance = new Library();
                    _libraryInstance.Init();
                }
                else
                    _libraryInstance = value;
            }
        }

        /// <summary>
        /// Colors for the theme library.
        /// </summary>
        [DataMember]
        public Colors Colors { get; protected set; }

        static Library()
        {
            if (Default == null)
            {
                Default = new Library();
                Default.Init();
            }
        }

        /// <summary>
        /// Seeds the library with the default themes.
        /// </summary>
        protected void Init()
        {
            //SetControlTheme(typeof())

            //ControlsConsoleTheme = new ControlsConsoleTheme(Colors);
            //WindowTheme = new WindowTheme(Colors);

            SetControlTheme(typeof(ScrollBar), new ScrollBarTheme());
            //ButtonTheme = new ButtonTheme();
            SetControlTheme(typeof(CheckBox), new CheckBoxTheme());
            //ListBoxTheme = new ListBoxTheme(new ScrollBarTheme());
            SetControlTheme(typeof(ProgressBar), new ProgressBarTheme());
            SetControlTheme(typeof(RadioButton), new RadioButtonTheme());
            //TextBoxTheme = new TextBoxTheme();
            //SelectionButtonTheme = new ButtonTheme();
            //DrawingSurfaceTheme = new DrawingSurfaceTheme();
            SetControlTheme(typeof(Button), new ButtonTheme());
            SetControlTheme(typeof(Label), new LabelTheme());
        }

        /// <summary>
        /// Creates a new instance of the theme library with default themes.
        /// </summary>
        public Library()
        {
            Colors = new Colors() { IsLibrary = true };
            _controlThemes = new Dictionary<Type, ThemeBase>(15);
        }

        /// <summary>
        /// Creates and returns a theme based on the type of control provided.
        /// </summary>
        /// <param name="control">The control instance</param>
        /// <returns>A theme that is associated with the control.</returns>
        public ThemeBase GetControlTheme(Type control)
        {
            if (_controlThemes.ContainsKey(control))
                return _controlThemes[control].Clone();

            throw new System.Exception("Control does not have an associated theme.");
        }

        /// <summary>
        /// Sets a control theme based on the control type.
        /// </summary>
        /// <param name="control">The control type to register a theme.</param>
        /// <param name="theme">The theme to associate with the control.</param>
        /// <returns>A theme that is associated with the control.</returns>
        public void SetControlTheme(Type control, ThemeBase theme)
        {
            if (null == control) throw new ArgumentNullException(nameof(control), "Cannot use a null control type");
            _controlThemes[control] = theme ?? throw new ArgumentNullException(nameof(theme), "Cannot set the theme of a control to null");
        }

        /// <summary>
        /// Clones this library.
        /// </summary>
        /// <returns>A new instance of a library.</returns>
        public Library Clone()
        {
            var library = new Library();

            foreach (var item in _controlThemes)
                library.SetControlTheme(item.Key, item.Value);

            library.Colors = Colors.Clone();
            library.Colors.IsLibrary = true;

            return library;
        }
    }
}
