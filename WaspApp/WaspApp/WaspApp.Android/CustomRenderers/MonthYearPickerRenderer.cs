using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using WaspApp.Controls;
using WaspApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MonthYearPickerView), typeof(MonthYearPickerRenderer))]
namespace WaspApp.Droid.CustomRenderers
{
    public class MonthYearPickerRenderer : ViewRenderer<MonthYearPickerView, EditText>
    {
        private readonly Context _context;
        private MonthYearPickerDialog _monthYearPickerDialog;

        public MonthYearPickerRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MonthYearPickerView> e)
        {
            base.OnElementChanged(e);

            CreateAndSetNativeControl();

            Control.KeyListener = null;
            Element.Focused += Element_Focused;

            if (e.NewElement != null)
                Draw(e.NewElement);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == MonthYearPickerView.DateProperty.PropertyName)
            {
                Control.Text = (sender as MonthYearPickerView).Date.ToString("Y");
                ClearPickerFocus();
            }

            var picker = sender as MonthYearPickerView;
            Draw(picker);
        }

        protected override void Dispose(bool disposing)
        {
            if (Control == null) return;

            Element.Focused -= Element_Focused;

            if (_monthYearPickerDialog != null)
            {
                _monthYearPickerDialog.OnDateTimeChanged -= OnDateTimeChanged;
                _monthYearPickerDialog.OnClosed -= OnClosed;
                _monthYearPickerDialog.Hide();
                _monthYearPickerDialog.Dispose();
                _monthYearPickerDialog = null;
            }

            base.Dispose(disposing);
        }

        #region Private Methods

        private void ShowDatePicker()
        {
            if (_monthYearPickerDialog == null)
            {
                _monthYearPickerDialog = new MonthYearPickerDialog();
                _monthYearPickerDialog.OnDateTimeChanged += OnDateTimeChanged;
                _monthYearPickerDialog.OnClosed += OnClosed;
            }
            _monthYearPickerDialog.Date = Element.Date;
            _monthYearPickerDialog.MinDate = FormatDateToMonthYear(Element.MinDate);
            _monthYearPickerDialog.MaxDate = FormatDateToMonthYear(Element.MaxDate);
            _monthYearPickerDialog.InfiniteScroll = Element.InfiniteScroll;

            var appcompatActivity = Xamarin.Essentials.Platform.CurrentActivity as AppCompatActivity;
            var mFragManager = appcompatActivity?.SupportFragmentManager;
            if (mFragManager != null)
            {
                _monthYearPickerDialog.Show(mFragManager, nameof(MonthYearPickerDialog));
            }
        }

        private void ClearPickerFocus()
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
            Control.ClearFocus();
        }

        private DateTime? FormatDateToMonthYear(DateTime? dateTime) =>
            dateTime.HasValue ? (DateTime?)new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1) : null;

        private void CreateAndSetNativeControl()
        {
            var tv = new EditText(_context);

            tv.SetTextColor(Element.TextColor.ToAndroid());
            tv.TextSize = (float)Element.FontSize;
            tv.Text = Element.Date.ToString("Y");
            tv.Gravity = Android.Views.GravityFlags.Start;
            tv.SetBackgroundColor(Element.BackgroundColor.ToAndroid());

            SetNativeControl(tv);
        }

        void Draw(MonthYearPickerView picker)
        {
            if (picker.BorderType == MonthYearPickerViewBorderType.Frame)
                DrawFrame(picker);
            else if (picker.BorderType == MonthYearPickerViewBorderType.Line)
                DrawLine(picker);
            else
                DrawNone();
        }

        void DrawFrame(MonthYearPickerView picker)
        {
            var bas = Control as TextView;

            //Get the measure to get the ViewSize
            bas.Measure(0, 0);

            //is common always get the heigh value instead of width, we can "pre define" this value as a reference
            var h = (picker.HeightRequest > 0 ? picker.HeightRequest : bas.MeasuredHeight / 2);

            var side = Math.Max(h, 0);
            var radius = side * picker.BorderRadius / 100;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(picker.BackgroundColor.ToAndroid());//background
            gd.SetCornerRadius((float)radius);//Border radius
            gd.SetStroke(picker.BorderStroke, picker.BorderColor.ToAndroid());//stroke
            Control.SetBackground(gd);
        }

        void DrawLine(MonthYearPickerView picker)
        {
            // Background drawable
            GradientDrawable backgroundDrawable = new GradientDrawable();
            backgroundDrawable.SetShape(ShapeType.Rectangle);
            backgroundDrawable.SetColor(Color.Transparent.ToAndroid());

            // Bottom line normal
            GradientDrawable lineNormalDrawable = new GradientDrawable();
            lineNormalDrawable.SetShape(ShapeType.Rectangle);
            lineNormalDrawable.SetColor(picker.BorderColor.ToAndroid());
            lineNormalDrawable.SetSize((int)Element.Width, (int)Helpers.Utilities.DpToPixels(Context, 1));

            // Creates layer to contain background drawable and bottom line drawable
            int verticalPadding = (int)Helpers.Utilities.DpToPixels(Context, 10);
            int horizontalPadding = (int)Helpers.Utilities.DpToPixels(Context, 5);
            Drawable[] drawables = new Drawable[] { backgroundDrawable, lineNormalDrawable };
            LayerDrawable normalLayer = new LayerDrawable(drawables);
            normalLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            normalLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)Helpers.Utilities.DpToPixels(Context, 8));
            normalLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates focused bottom line drawable
            GradientDrawable lineFocusedDrawable = new GradientDrawable();
            lineFocusedDrawable.SetShape(ShapeType.Rectangle);
            lineFocusedDrawable.SetColor(picker.FocusedBorderColor.ToAndroid());
            lineFocusedDrawable.SetSize((int)Element.Width, (int)Helpers.Utilities.DpToPixels(Context, 2));

            // Creates layer to contain background and focus drawable layers
            drawables = new Drawable[] { backgroundDrawable, lineFocusedDrawable };
            LayerDrawable focusLayer = new LayerDrawable(drawables);
            focusLayer.SetPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
            focusLayer.SetLayerInset(1, horizontalPadding, 0, horizontalPadding, (int)Helpers.Utilities.DpToPixels(Context, 8));
            focusLayer.SetLayerGravity(1, Android.Views.GravityFlags.Bottom);

            // Creates drawable state list
            StateListDrawable state = new StateListDrawable();
            state.AddState(new int[] { Android.Resource.Attribute.StateFocused }, focusLayer);
            state.AddState(new int[] { }, normalLayer);

            Control.SetBackground(state);
        }

        void DrawNone()
        {
            Control.SetBackground(null);
        }
        #endregion

        #region Event Handlers

        private void Element_Focused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                ShowDatePicker();
            }
        }

        private void OnClosed(object sender, DateTime e)
        {
            ClearPickerFocus();
        }

        private void OnDateTimeChanged(object sender, DateTime e)
        {
            Element.Date = e;
            Control.Text = e.ToString("Y");
            ClearPickerFocus();
        }

        #endregion
    }

    public class MonthYearPickerDialog : AndroidX.Fragment.App.DialogFragment
    {
        public event EventHandler<DateTime> OnDateTimeChanged;
        public event EventHandler<DateTime> OnClosed;

        #region Private Fields

        private const int DefaultDay = 1;
        private const int MinNumberOfMonths = 1;
        private const int MaxNumberOfMonths = 12;
        private const int MinNumberOfYears = 1900;
        private const int MaxNumberOfYears = 2100;

        private NumberPicker _monthPicker;
        private NumberPicker _yearPicker;

        #endregion

        #region Public Properties

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public DateTime? Date { get; set; }
        public bool InfiniteScroll { get; set; }

        #endregion

        public void Hide() => base.Dialog?.Hide();

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var builder = new AndroidX.AppCompat.App.AlertDialog.Builder(Activity);
            var inflater = Activity.LayoutInflater;

            var selectedDate = GetSelectedDate();

            var dialog = inflater.Inflate(Resource.Layout.date_picker_dialog, null);
            _monthPicker = (NumberPicker)dialog.FindViewById(Resource.Id.picker_month);
            _yearPicker = (NumberPicker)dialog.FindViewById(Resource.Id.picker_year);

            InitializeMonthPicker(selectedDate.Month);
            InitializeYearPicker(selectedDate.Year);
            SetMaxMinDate(MaxDate, MinDate);

            builder.SetView(dialog)
                .SetPositiveButton("Ok", (sender, e) =>
                {
                    selectedDate = new DateTime(_yearPicker.Value, _monthPicker.Value, DefaultDay);
                    OnDateTimeChanged?.Invoke(dialog, selectedDate);
                })
                .SetNegativeButton("Cancel", (sender, e) =>
                {
                    Dialog.Cancel();
                    OnClosed?.Invoke(dialog, selectedDate);
                });
            return builder.Create();
        }

        protected override void Dispose(bool disposing)
        {
            if (_yearPicker != null)
            {
                _yearPicker.ScrollChange -= YearPicker_ScrollChange;
                _yearPicker.Dispose();
                _yearPicker = null;
            }

            _monthPicker?.Dispose();
            _monthPicker = null;


            base.Dispose(disposing);
        }

        #region Private Methods

        private DateTime GetSelectedDate() => Date ?? DateTime.Now;

        private void InitializeYearPicker(int year)
        {
            _yearPicker.MinValue = MinNumberOfYears;
            _yearPicker.MaxValue = MaxNumberOfYears;
            _yearPicker.Value = year;
            _yearPicker.ScrollChange += YearPicker_ScrollChange;
            if (!InfiniteScroll)
            {
                _yearPicker.WrapSelectorWheel = false;
                _yearPicker.DescendantFocusability = DescendantFocusability.BlockDescendants;
            }
        }

        private void InitializeMonthPicker(int month)
        {
            _monthPicker.MinValue = MinNumberOfMonths;
            _monthPicker.MaxValue = MaxNumberOfMonths;
            _monthPicker.SetDisplayedValues(GetMonthNames());
            _monthPicker.Value = month;
            if (!InfiniteScroll)
            {
                _monthPicker.WrapSelectorWheel = false;
                _monthPicker.DescendantFocusability = DescendantFocusability.BlockDescendants;
            }
        }

        private void YearPicker_ScrollChange(object sender, Android.Views.View.ScrollChangeEventArgs e)
        {
            SetMaxMinDate(MaxDate, MinDate);
        }

        private void SetMaxMinDate(DateTime? maxDate, DateTime? minDate)
        {
            try
            {
                if (maxDate.HasValue)
                {
                    var maxYear = maxDate.Value.Year;
                    var maxMonth = maxDate.Value.Month;

                    if (_yearPicker.Value == maxYear)
                    {
                        _monthPicker.MaxValue = maxMonth;
                    }
                    else if (_monthPicker.MaxValue != MaxNumberOfMonths)
                    {
                        _monthPicker.MaxValue = MaxNumberOfMonths;
                    }

                    _yearPicker.MaxValue = maxYear;
                }

                if (minDate.HasValue)
                {
                    var minYear = minDate.Value.Year;
                    var minMonth = minDate.Value.Month;

                    if (_yearPicker.Value == minYear)
                    {
                        _monthPicker.MinValue = minMonth;
                    }
                    else if (_monthPicker.MinValue != MinNumberOfMonths)
                    {
                        _monthPicker.MinValue = MinNumberOfMonths;
                    }

                    _yearPicker.MinValue = minYear;
                }
                _monthPicker.SetDisplayedValues(GetMonthNames(_monthPicker.MinValue));
            }
            catch (Exception e)
            {

            }
        }

        private string[] GetMonthNames(int start = 1) =>
            System.Globalization.DateTimeFormatInfo.CurrentInfo?.MonthNames.Skip(start - 1).ToArray();

        #endregion
    }
}