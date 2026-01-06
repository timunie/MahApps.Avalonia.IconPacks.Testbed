using System;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace MahApps.IconPacksBrowser.Avalonia.Controls;

// Migrated from https://github.com/PieroCastillo/Aura.UI/tree/master/src/Aura.UI/Controls/CardCollection

public class CardControl: HeaderedContentControl, ICommandSource
    {
        private bool _commandCanExecute;

        static CardControl()
        {
            ClickModeProperty.OverrideDefaultValue<CardControl>(ClickMode.Release);
        }
        public object SecondaryHeader
        {
            get => GetValue(SecondaryHeaderProperty);
            set => SetValue(SecondaryHeaderProperty, value);
        }

        public static readonly StyledProperty<object> SecondaryHeaderProperty =
            AvaloniaProperty.Register<CardControl, object>(nameof(SecondaryHeader));

        public ITemplate SecondaryHeaderTemplate
        {
            get => GetValue(SecondaryHeaderTemplateProperty);
            set => SetValue(SecondaryHeaderTemplateProperty, value);
        }

        public static readonly StyledProperty<ITemplate> SecondaryHeaderTemplateProperty =
            AvaloniaProperty.Register<CardControl, ITemplate>(nameof(SecondaryHeaderTemplate));

        public IBrush SecondaryBackground
        {
            get => GetValue(SecondaryBackgroundProperty);
            set => SetValue(SecondaryBackgroundProperty, value);
        }

        public static readonly StyledProperty<IBrush> SecondaryBackgroundProperty =
            AvaloniaProperty.Register<CardControl, IBrush>(nameof(SecondaryBackground));

        public bool ScaleOnPointerOver
        {
            get => GetValue(ScaleOnPointerOverProperty);
            set => SetValue(ScaleOnPointerOverProperty, value);
        }

        public static readonly StyledProperty<bool> ScaleOnPointerOverProperty =
            AvaloniaProperty.Register<CardControl, bool>(nameof(ScaleOnPointerOver));

        public BoxShadows BoxShadow
        {
            get => GetValue(BoxShadowProperty);
            set => SetValue(BoxShadowProperty, value);
        }

        public static readonly StyledProperty<BoxShadows> BoxShadowProperty =
            AvaloniaProperty.Register<CardControl, BoxShadows>(nameof(BoxShadow));
        
        
        /// <summary>
        /// Defines the Uniform CornerRadius
        /// </summary>
        public new CornerRadius CornerRadius
        {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static new readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<CardControl, CornerRadius>(nameof(CornerRadius), new CornerRadius(7));

        /// <summary>
        /// Defines the Top CornerRadius
        /// </summary>
        public CornerRadius TopCornerRadius
        {
            get => GetValue(TopCornerRadiusProperty);
            set => SetValue(TopCornerRadiusProperty, value);
        }

        public static readonly StyledProperty<CornerRadius> TopCornerRadiusProperty =
            AvaloniaProperty.Register<CardControl, CornerRadius>(nameof(TopCornerRadius), new CornerRadius(7, 0));

        /// <summary>
        /// Defines the Bottom CornerRadius
        /// </summary>
        public CornerRadius BottomCornerRadius
        {
            get => GetValue(BottomCornerRadiusProperty);
            set => SetValue(BottomCornerRadiusProperty, value);
        }

        public static readonly StyledProperty<CornerRadius> BottomCornerRadiusProperty =
            AvaloniaProperty.Register<CardControl, CornerRadius>(nameof(BottomCornerRadius), new CornerRadius(0, 7));

        public CornerRadius InternalCornerRadius
        {
            get => GetValue(InternalCornerRadiusProperty);
            set => SetValue(InternalCornerRadiusProperty, value);
        }

        public static readonly StyledProperty<CornerRadius> InternalCornerRadiusProperty =
            AvaloniaProperty.Register<CardControl, CornerRadius>(nameof(InternalCornerRadius), new CornerRadius(7));

        public Thickness InternalPadding
        {
            get => GetValue(InternalPaddingProperty);
            set => SetValue(InternalPaddingProperty, value);
        }

        public static readonly StyledProperty<Thickness> InternalPaddingProperty =
            AvaloniaProperty.Register<CardControl, Thickness>(nameof(InternalPadding));
        
        
        public BoxShadows InternalBoxShadow
        {
            get => GetValue(InternalBoxShadowProperty);
            set => SetValue(InternalBoxShadowProperty, value);
        }

        public static readonly StyledProperty<BoxShadows> InternalBoxShadowProperty =
            AvaloniaProperty.Register<CardControl, BoxShadows>(nameof(InternalBoxShadow));
        
        public ICommand? Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly StyledProperty<ICommand?> CommandProperty =
            Button.CommandProperty.AddOwner<CardControl>();

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly StyledProperty<object?> CommandParameterProperty =
            Button.CommandParameterProperty.AddOwner<CardControl>();

        public ClickMode ClickMode
        {
            get => GetValue(ClickModeProperty);
            set => SetValue(ClickModeProperty, value);
        }
        public static readonly StyledProperty<ClickMode> ClickModeProperty =
            Button.ClickModeProperty.AddOwner<CardControl>();

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == CommandProperty)
            {
                if (e.OldValue is ICommand oldCommand)
                {
                    oldCommand.CanExecuteChanged -= CanExecuteChanged;
                }

                if (e.NewValue is ICommand newCommand)
                {
                    newCommand.CanExecuteChanged += CanExecuteChanged;
                }
            }

            if (e.Property == CommandParameterProperty)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed &&
                ClickMode == ClickMode.Press)
            {
                OnClick();
            }
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            if (e.InitialPressMouseButton == MouseButton.Left &&
                ClickMode == ClickMode.Release &&
                this.GetVisualsAt(e.GetPosition(this)).Any(c => this == c || this.IsVisualAncestorOf(c)))
            {
                OnClick();
            }
        }

        void CanExecuteChanged(object? sender, EventArgs e)
        {
            var canExecute = Command == null || Command.CanExecute(CommandParameter);
            
            _commandCanExecute = canExecute;
        }

        protected virtual void OnClick()
        {
            if (Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }
        }

        protected override void UpdateDataValidation(AvaloniaProperty property, BindingValueType state, Exception? error)
        {
            base.UpdateDataValidation(property, state, error);
            
            if (property == CommandProperty)
            {
                if (state == BindingValueType.BindingError)
                {
                    if (_commandCanExecute)
                    {
                        _commandCanExecute = false;
                        //UpdateIsEffectivelyEnabled();
                    }
                }
            }
        }

        void ICommandSource.CanExecuteChanged(object sender, EventArgs e) => CanExecuteChanged(sender, e);
}