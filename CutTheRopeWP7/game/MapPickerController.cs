using System.Collections.Generic;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;
using ctr_wp7.wp7utilities;

namespace ctr_wp7.game
{
    internal sealed class MapPickerController : ViewController, ButtonDelegate
    {
        public override NSObject initWithParent(ViewController p)
        {
            if (base.initWithParent(p) != null)
            {
                selectedMap = null;
                maplist = null;
                createPickerView();
                View view = (View)new View().initFullscreen();
                RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
                rectangleElement.color = RGBAColor.whiteRGBA;
                rectangleElement.width = (int)SCREEN_WIDTH;
                rectangleElement.height = (int)SCREEN_HEIGHT;
                _ = view.addChild(rectangleElement);
                FontGeneric font = Application.getFont(6);
                Text text = new Text().initWithFont(font);
                text.setString(NSS("Loading..."));
                text.anchor = text.parentAnchor = 18;
                _ = view.addChild(text);
                addViewwithID(view, 1);
                setNormalMode();
            }
            return this;
        }

        public static NSString getLevelNameForPackLevel(int pack, int level)
        {
            return NSS((pack + 1).ToString() + "_" + (level + 1).ToString() + ".xml");
        }

        public void createPickerView()
        {
            View view = (View)new View().initFullscreen();
            RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
            rectangleElement.color = RGBAColor.whiteRGBA;
            rectangleElement.width = (int)SCREEN_WIDTH;
            rectangleElement.height = (int)SCREEN_HEIGHT;
            _ = view.addChild(rectangleElement);
            FontGeneric font = Application.getFont(6);
            Text text = new Text().initWithFont(font);
            text.setString(NSS("START"));
            Text text2 = new Text().initWithFont(font);
            text2.setString(NSS("START"));
            text2.scaleX = text2.scaleY = 1.2f;
            Button button = new Button().initWithUpElementDownElementandID(text, text2, 0);
            button.anchor = button.parentAnchor = 34;
            button.delegateButtonDelegate = this;
            _ = view.addChild(button);
            addViewwithID(view, 0);
        }

        public override void activate()
        {
            base.activate();
            if (autoLoad)
            {
                NSString nsstring = NSS("maps/" + selectedMap);
                XMLNode xmlnode = XMLNode.parseXML(nsstring.ToString());
                xmlLoaderFinishedWithfromwithSuccess(xmlnode, nsstring, xmlnode != null);
                return;
            }
            showView(0);
            loadList();
        }

        public static void loadList()
        {
        }

        public override void deactivate()
        {
            base.deactivate();
        }

        public void xmlLoaderFinishedWithfromwithSuccess(XMLNode rootNode, NSString url, bool success)
        {
            if (rootNode != null)
            {
                CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
                if (autoLoad)
                {
                    CTRRootController.checkMapIsValid(ContentHelper.OpenResourceAsString(url.ToString()).ToCharArray());
                }
                ctrrootController.setMap(rootNode);
                ctrrootController.setMapName(selectedMap);
                CTRRootController.setMapsList(maplist);
                deactivate();
            }
        }

        public void setNormalMode()
        {
            autoLoad = false;
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            ctrrootController.setPicker(true);
        }

        public void setAutoLoadMap(NSString map)
        {
            autoLoad = true;
            CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
            ctrrootController.setPicker(false);
            NSREL(selectedMap);
            selectedMap = (NSString)NSRET(map);
        }

        public void onButtonPressed(int n)
        {
            if (n != 0)
            {
                return;
            }
            loadList();
        }

        public override void dealloc()
        {
            NSREL(selectedMap);
            base.dealloc();
        }

        private NSString selectedMap;

        private Dictionary<string, XMLNode> maplist;

        private bool autoLoad;
    }
}
