using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BullsAndCows.Behaviours
{
    /// <summary>
    /// Клас за валидация на въведените от потребителя данни по зададен Regex в TextBox
    /// </summary>
    public class TextBoxInputBehavior : BaseTextInputBehavior<TextBox>
    {
        /// <summary> Свойство за TextBox </summary>
        protected override TextBox AssociatedTextBox
        {
            get
            {
                return this.AssociatedObject;
            }
        }
    }

    /// <summary>
    /// Клас с Regex
    /// </summary>
    public static class ValidationExpression
    {
        /// <summary> Константа с текст за валидиране на целочислено число </summary>
        public const string Integer = @"^[\-]?[0-9]+$";

        /// <summary> Константа с текст за валидиране на цяло положително число </summary>
        public const string PositiveInteger = @"[0-9]+$";

        /// <summary> Константа с текст за валидиране на дробно число </summary>
        public const string Double = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]+$)))";

        /// <summary> Константа с текст за валидиране на цяло дробно число </summary>
        public const string PositiveDecimal = @"(^[0-9]*)($|\.$|(.[0-9]+$))";

        /// <summary> Константа с текст за валидиране на цяло дробно число с 2 знака след запетаята </summary>
        public const string PositiveDecimalPrecision2 = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]{0,2}$)))";

        /// <summary> Константа с текст за валидиране на цяло дробно число с 3 знака след запетаята </summary>
        public const string PositiveDecimalPrecision3 = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]{0,3}$)))";

        /// <summary> Константа с текст за валидиране на цяло дробно число с 4 знака след запетаята </summary>
        public const string PositiveDecimalPrecision4 = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]{0,4}$)))";

        /// <summary> Константа с текст за валидиране на цяло дробно число с 5 знака след запетаята </summary>
        public const string PositiveDecimalPrecision5 = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]{0,5}$)))";

        /// <summary> Константа с текст за валидиране на цяло дробно число с 6 знака след запетаята </summary>
        public const string PositiveDecimalPrecision6 = @"(^[\-]?)($|[0-9]*($|\.($|[0-9]{0,6}$)))";

        /// <summary> Константа с текст за валидиране на положителен процент с точност до 2 знака след запетаята </summary>
        public const string PositivePercentPrecision2 = @"(^)($|100$|100\.$|100\.0$|100\.00$|[0-9]{0,2}($|\.($|[0-9]{0,2}$)))";
    }
}
