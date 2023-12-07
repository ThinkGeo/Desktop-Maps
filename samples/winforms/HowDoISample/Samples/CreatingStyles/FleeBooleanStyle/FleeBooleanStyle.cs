using Jint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    [Serializable]
    public class FleeBooleanStyle : Style
    {
        [Obfuscation(Exclude = true)]
        private string fleeExpression;
        [Obfuscation(Exclude = true)]
        private Collection<Style> customTrueStyles;
        [Obfuscation(Exclude = true)]
        private Collection<Style> customFalseStyles;
        [Obfuscation(Exclude = true)]
        private Dictionary<string, object> userVariables;
        [Obfuscation(Exclude = true)]
        private Collection<string> columnVariables;
        [Obfuscation(Exclude = true)]
        private Collection<Type> staticTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="FleeBooleanStyle"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor for the style and should be called by inherited
        /// classes.
        /// </remarks>
        /// <returns>None</returns>
        public FleeBooleanStyle()
            : this(string.Empty, new Collection<Style>(), new Collection<Style>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FleeBooleanStyle"/> class.
        /// </summary>
        /// <param name="fleeExpression">The flee expression.</param>
        public FleeBooleanStyle(string fleeExpression)
            : this(fleeExpression, new Collection<Style>(), new Collection<Style>())
        {
        }

        private FleeBooleanStyle(string fleeExpression, Collection<Style> trueCustomStyles, Collection<Style> falseCustomStyles)
        {
            this.fleeExpression = fleeExpression;
            customTrueStyles = trueCustomStyles;
            customFalseStyles = falseCustomStyles;
            userVariables = new Dictionary<string, object>();
            columnVariables = new Collection<string>();
            staticTypes = new Collection<Type>();
        }

        /// <summary>
        /// Gets or sets the flee expression.
        /// </summary>
        /// <value>
        /// The flee expression.
        /// </value>
        public string FleeExpression
        {
            get => fleeExpression;
            set => fleeExpression = value;
        }

        /// <summary>
        /// Gets the static types.
        /// </summary>
        /// <value>
        /// The static types.
        /// </value>
        public Collection<Type> StaticTypes => staticTypes;

        /// <summary>
        /// Gets the column variables.
        /// </summary>
        /// <value>
        /// The column variables.
        /// </value>
        public Collection<string> ColumnVariables => columnVariables;

        /// <summary>
        /// Gets the user variables.
        /// </summary>
        /// <value>
        /// The user variables.
        /// </value>
        public Dictionary<string, object> UserVariables => userVariables;

        /// <summary>
        /// Gets the custom true styles.
        /// </summary>
        /// <value>
        /// The custom true styles.
        /// </value>
        public Collection<Style> CustomTrueStyles => customTrueStyles;

        /// <summary>
        /// Gets the custom false styles.
        /// </summary>
        /// <value>
        /// The custom false styles.
        /// </value>
        public Collection<Style> CustomFalseStyles => customFalseStyles;

        /// <summary>
        /// This method draws the features on the view you provided.
        /// </summary>
        /// <param name="features">This parameter represents the features you want to draw on the view.</param>
        /// <param name="canvas">This parameter represents the view you want to draw the features on.</param>
        /// <param name="labelsInThisLayer">The labels will be drawn in the current layer only.</param>
        /// <param name="labelsInAllLayers">The labels will be drawn in all layers.</param>
        /// <remarks>
        /// This abstract method is called from the concrete public method Draw. In this
        /// method, we take the features you passed in and draw them on the view you provided.
        /// Each style (based on its properties) may draw each feature differently.<br /><br /><br />
        /// When implementing this abstract method, consider each feature and its column data
        /// values. You can use the full power of the GeoCanvas to do the drawing. If you need
        /// column data for a feature, be sure to override the GetRequiredColumnNamesCore and add
        /// the columns you need to the collection. In many of the styles, we add properties to
        /// allow the user to specify which field they need; then, in the GetRequiredColumnNamesCore,
        /// we read that property and add it to the collection.
        /// </remarks>
        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            var engine = new Engine();
            // Add all of the user variables
            foreach (string customVariableKey in userVariables.Keys)
            {
                engine.SetValue(customVariableKey, userVariables[customVariableKey]);
            }
            // Add all of the column variables
            foreach (string columnVariable in columnVariables)
            {
                engine.SetValue(columnVariable, string.Empty);
            }

            foreach (Feature feature in features)
            {
                if (canvas.CancellationToken.IsCancellationRequested)
                    return;

                // update the variables we get from the feature
                foreach (string columnName in columnVariables)
                {
                    engine.SetValue(columnName, feature.ColumnValues[columnName]);
                }

                bool evaluatedTrue = engine.Execute(fleeExpression).GetCompletionValue().AsBoolean();

                if (evaluatedTrue)
                {
                    foreach (Style style in customTrueStyles)
                    {
                        if (canvas.CancellationToken.IsCancellationRequested)
                            return;

                        style.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                }
                else
                {
                    foreach (Style style in customFalseStyles)
                    {
                        if (canvas.CancellationToken.IsCancellationRequested)
                            return;

                        style.Draw(new Collection<Feature>() { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
                    }
                }
            }
        }

        /// <summary>
        /// This method returns the column data for each feature that is required for the
        /// style to properly draw.
        /// </summary>
        /// <returns>
        /// This method returns a collection of column names that the style needs.
        /// </returns>
        /// <remarks>
        /// This abstract method is called from the concrete public method
        /// GetRequiredFieldNames. In this method, we return the column names that are required for
        /// the style to draw the feature properly. For example, if you have a style that colors
        /// areas blue when a certain column value is over 100, then you need to be sure you include
        /// that column name. This will ensure that the column data is returned to you in the
        /// feature when it is ready to draw.<br /><br />
        /// In many of the styles, we add properties to allow the user to specify which field they
        /// need; then, in the GetRequiredColumnNamesCore we read that property and add it to the
        /// collection.
        /// </remarks>
        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            var requiredFieldNames = new Collection<string>();

            // Column Variables
            foreach (var columnName in columnVariables)
            {
                if (!requiredFieldNames.Contains(columnName))
                {
                    requiredFieldNames.Add(columnName);
                }
            }

            // Custom True Styles
            foreach (var style in customTrueStyles)
            {
                var tmpCollection = style.GetRequiredColumnNames();

                foreach (var name in tmpCollection)
                {
                    if (!requiredFieldNames.Contains(name))
                    {
                        requiredFieldNames.Add(name);
                    }
                }
            }

            // Custom False Styles
            foreach (var style in customFalseStyles)
            {
                var tmpCollection = style.GetRequiredColumnNames();

                foreach (var name in tmpCollection)
                {
                    if (!requiredFieldNames.Contains(name))
                    {
                        requiredFieldNames.Add(name);
                    }
                }
            }

            return requiredFieldNames;
        }
    }
}
