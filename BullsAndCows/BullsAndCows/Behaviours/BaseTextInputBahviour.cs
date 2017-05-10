using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BullsAndCows.Behaviours
{
    /// <summary>
    /// Клас за валидация на въведените от потребителя данни по зададен Regex
    /// </summary>
    /// <typeparam name="T">Тип на визуалния елемент, който се валидира</typeparam>
    public abstract class BaseTextInputBehavior<T> : Behavior<T> where T : UIElement
    {
        #region Declarations

        /// <summary> Променлива с Regex </summary>
        private Regex regеx;

        /// <summary> Binding за анимация </summary>
        private Binding animatedPropertyBinding = null;

        /// <summary> MultiBinding за анимация </summary>
        private MultiBinding animatedPropertyMultiBinding = null;

        #endregion

        #region Properties

        /// <summary>
        /// Свойство за визуален елемент, в който се въвеждат данните
        /// </summary>
        protected abstract TextBox AssociatedTextBox { get; }

        /// <summary>
        /// Свойство за Regex
        /// </summary>
        public string RegularExpression
        {
            get
            {
                return (string)this.GetValue(RegularExpressionProperty);
            }

            set
            {
                this.SetValue(RegularExpressionProperty, value);
            }
        }

        /// <summary>
        /// Свойство за цвят при неправилно въведени данни
        /// </summary>
        public Color AnimationBrushColor
        {
            get
            {
                return (Color)this.GetValue(AnimationBrushColorProperty);
            }

            set
            {
                this.SetValue(AnimationBrushColorProperty, value);
            }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// DependencyProperty за цвят при неправилно въведени данни
        /// </summary>
        public static DependencyProperty AnimationBrushColorProperty =
            DependencyProperty.Register("AnimationBrushColor", typeof(Color), typeof(BaseTextInputBehavior<T>));

        /// <summary>
        /// DependencyProperty за Regex
        /// </summary>
        public static DependencyProperty RegularExpressionProperty =
            DependencyProperty.Register("RegularExpression", typeof(string), typeof(BaseTextInputBehavior<T>), new PropertyMetadata(RegExPropertyChanged));

        #endregion

        #region Override Methods

        /// <summary>
        /// Метод, който инициализира събитията на Behavior-а
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewKeyDown += RegExTextBox_PreviewKeyDown;
            this.AssociatedObject.PreviewTextInput += RegExTextBox_PreviewTextInput;
            DataObject.AddPastingHandler(this.AssociatedObject, new DataObjectPastingEventHandler(DataObjectPasting));
        }

        /// <summary>
        /// Метод, който премахва събитията на Behavior-а
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewKeyDown -= RegExTextBox_PreviewKeyDown;
            this.AssociatedObject.PreviewTextInput -= RegExTextBox_PreviewTextInput;
            DataObject.RemovePastingHandler(this.AssociatedObject, new DataObjectPastingEventHandler(DataObjectPasting));
        }

        #endregion // Override Methods

        #region UI Event Handlers

        /// <summary>
        /// Метод, който се извиква при натискане на клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegExTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                string newValue = this.AssociatedTextBox.Text.Insert(this.AssociatedTextBox.SelectionStart, " ");

                if (!Validate(newValue))
                    e.Handled = true;
            }
        }

        /// <summary>
        /// Метод, който се извиква при въвеждане на текст
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegExTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newValue = this.AssociatedTextBox.Text.Remove(
                this.AssociatedTextBox.SelectionStart,
                this.AssociatedTextBox.SelectionLength);

            newValue = newValue.Insert(this.AssociatedTextBox.SelectionStart, e.Text);

            if (!Validate(newValue))
            {
                e.Handled = true;
                BeginInvalidInputAnimation();
            }
        }

        /// <summary>
        /// Метод, който се извиква при Paste на текст във визуалния елемент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataObjectPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string textToPaste = e.DataObject.GetData(typeof(string)) as string;
                if (textToPaste != null)
                {
                    textToPaste = textToPaste.Replace("\r\n", string.Empty);
                    string newValue = this.AssociatedTextBox.Text.Remove(
                        this.AssociatedTextBox.SelectionStart,
                        this.AssociatedTextBox.SelectionLength);

                    newValue = newValue.Insert(this.AssociatedTextBox.SelectionStart, textToPaste);

                    if (!Validate(newValue))
                        e.CancelCommand();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Метод, който се извиква при промяна на стойността на Regex
        /// </summary>
        /// <param name="hostControl"></param>
        /// <param name="e"></param>
        private static void RegExPropertyChanged(DependencyObject hostControl, DependencyPropertyChangedEventArgs e)
        {
            string newRegEx = e.NewValue as string;
            BaseTextInputBehavior<T> behavior = hostControl as BaseTextInputBehavior<T>;
            if (!string.IsNullOrEmpty(newRegEx))
                behavior.regеx = new Regex(newRegEx);
            else
                behavior.regеx = null;
        }

        /// <summary>
        /// Метод, който проверява въведениея текст дали отговаря на Regex-а
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private bool Validate(string newValue)
        {
            if (this.regеx == null)
                return true;

            return this.regеx.IsMatch(newValue);
        }

        /// <summary>
        /// Метод, който визуализира цвета при неправилно въведени данни
        /// </summary>
        private void BeginInvalidInputAnimation()
        {
            ColorAnimation animation = new ColorAnimation(this.AnimationBrushColor, TimeSpan.FromSeconds(0.1))
            {
                AutoReverse = true,
                FillBehavior = FillBehavior.Stop,
                DecelerationRatio = 0.8
            };

            // Ако свойството за Background участва в Binding, няма да може да бъде анимирано, затова временно 
            // откачаме Binding-а и го възстановяваме след края на анимацията (в метода storyBoard_Completed())
            if (BindingOperations.IsDataBound(this.AssociatedTextBox, Control.BackgroundProperty))
            {
                this.animatedPropertyBinding = BindingOperations.GetBinding(this.AssociatedTextBox, Control.BackgroundProperty);
                this.animatedPropertyMultiBinding = BindingOperations.GetMultiBinding(this.AssociatedTextBox, Control.BackgroundProperty);

                if (this.animatedPropertyBinding != null || this.animatedPropertyMultiBinding != null)
                {
                    // задаваме текущата стойност локално
                    Brush currentValue = this.AssociatedObject.GetValue(Control.BackgroundProperty) as Brush;
                    this.AssociatedTextBox.Background = currentValue;
                }
            }

            Storyboard.SetTargetProperty(animation, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
            Storyboard storyBoard = new Storyboard();
            storyBoard.Children.Add(animation);

            storyBoard.Completed += new EventHandler(StoryBoard_Completed);
            storyBoard.Begin(this.AssociatedTextBox);

            SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Метод, който опреснява binding-a за цвета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            if (this.animatedPropertyBinding != null)
            {
                this.AssociatedTextBox.SetBinding(Control.BackgroundProperty, this.animatedPropertyBinding);
                this.animatedPropertyBinding = null;
            }

            if (this.animatedPropertyMultiBinding != null)
            {
                this.AssociatedTextBox.SetBinding(Control.BackgroundProperty, this.animatedPropertyMultiBinding);
                this.animatedPropertyMultiBinding = null;
            }
        }

        #endregion
    }
}
