using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupMaui
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        private Button popupDisplayButton;
        private SfPopup sfPopup;
        private View popupOverlayView;

        #endregion

        #region Overrides

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            popupDisplayButton = bindable.FindByName<Button>("displaybutton");
            sfPopup = bindable.FindByName<SfPopup>("sfPopup");
            sfPopup.Opening += OnSfPopupOpening;
            popupDisplayButton.Clicked += OnPopupDisplayButtonClicked;
        }
       
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            popupDisplayButton.Clicked -= OnPopupDisplayButtonClicked;
            sfPopup.Opening -= OnSfPopupOpening;
            popupOverlayView.PropertyChanged -= OnOverlayPropertyChanged;
            sfPopup = null;
            popupDisplayButton = null;
            popupOverlayView = null;
        }

        #endregion

        #region Private Methods

        private void OnPopupDisplayButtonClicked(object? sender, EventArgs e)
        {
            sfPopup.Show();
        }

        private void OnOverlayPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Opacity")
            {
                popupOverlayView.Opacity = 1;
            }
        }

        private void OnSfPopupOpening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var propertyInfo = sfPopup.GetType().GetField("PopupOverlayContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            popupOverlayView = propertyInfo.GetValue(sfPopup) as View;
            popupOverlayView.PropertyChanged += OnOverlayPropertyChanged;
        }

        #endregion

    }
}
