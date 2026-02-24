using ctr_wp7.ctr_commons;
using ctr_wp7.ctr_original;
using ctr_wp7.game;
using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace ctr_wp7.Specials
{
    internal sealed class UpdatePopup : Popup, ButtonDelegate
    {
        public void onButtonPressed(int n)
        {
            switch (n)
            {
                case 0:
                    hidePopup();
                    return;
                case 1:
                    Guide.ShowMarketplace(PlayerIndex.One);
                    return;
                default:
                    return;
            }
        }

        public static void showUpdatePopup()
        {
            bool flag = false;
            switch (Application.sharedRootController().activeChildID)
            {
                case 0:
                    App.NeedsUpdate = true;
                    return;
                case 1:
                    flag = false;
                    break;
                case 2:
                    App.NeedsUpdate = true;
                    return;
                case 3:
                    flag = true;
                    break;
            }
            App.NeedsUpdate = false;
            UpdatePopup updatePopup = (UpdatePopup)new UpdatePopup().init();
            updatePopup.setName(NSS("popup"));
            Button button = MenuController.createButtonWithTextIDDelegate(Application.getString(1310748), 0, updatePopup);
            button.anchor = button.parentAnchor = 18;
            button.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            Button button2 = MenuController.createButtonWithTextIDDelegate(Application.getString(1310747), 1, updatePopup);
            button2.anchor = 33;
            button2.parentAnchor = 9;
            button2.setTouchIncreaseLeftRightTopBottom(15f, 15f, 0f, 0f);
            _ = button.addChild(button2);
            FontGeneric font = Application.getFont(5);
            float num = 300f;
            if (LANGUAGE == Language.LANG_KO)
            {
                num /= 0.85f;
            }
            Text text = new Text().initWithFont(font);
            text.setAlignment(2);
            text.setStringandWidth(Application.getString(1310828), num);
            text.y = -34f;
            text.scaleX = text.scaleY = 0.8f;
            if (LANGUAGE == Language.LANG_KO)
            {
                text.scaleX *= 0.85f;
                text.scaleY *= 0.85f;
            }
            Image image = Image.Image_createWithResIDQuad(68, 0);
            image.anchor = image.parentAnchor = 18;
            _ = updatePopup.addChild(image);
            text.anchor = text.parentAnchor = 18;
            _ = image.addChild(text);
            button.y += -14f;
            button.anchor = 18;
            button.parentAnchor = 34;
            _ = image.addChild(button);
            updatePopup.showPopup();
            ViewController currentController = Application.sharedRootController().getCurrentController();
            View view = currentController.activeView();
            _ = view.addChild(updatePopup);
            if (flag)
            {
                ((GameController)currentController).setPaused(true);
                return;
            }
            MenuController.ep = updatePopup;
        }

        private const int buttonNo = 0;

        private const int buttonYes = 1;
    }
}
