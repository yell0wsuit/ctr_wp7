using System;
using System.Collections.Generic;
using ctre_wp7.iframework;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.visual;
using ctre_wp7.ios;
using ctre_wp7.wp7utilities;

namespace ctre_wp7.game
{
	// Token: 0x0200009C RID: 156
	internal class MapPickerController : ViewController, ButtonDelegate
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00021814 File Offset: 0x0001FA14
		public override NSObject initWithParent(ViewController p)
		{
			if (base.initWithParent(p) != null)
			{
				this.selectedMap = null;
				this.maplist = null;
				this.createPickerView();
				View view = (View)new View().initFullscreen();
				RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
				rectangleElement.color = RGBAColor.whiteRGBA;
				rectangleElement.width = (int)FrameworkTypes.SCREEN_WIDTH;
				rectangleElement.height = (int)FrameworkTypes.SCREEN_HEIGHT;
				view.addChild(rectangleElement);
				FontGeneric font = Application.getFont(6);
				Text text = new Text().initWithFont(font);
				text.setString(NSObject.NSS("Loading..."));
				text.anchor = (text.parentAnchor = 18);
				view.addChild(text);
				this.addViewwithID(view, 1);
				this.setNormalMode();
			}
			return this;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000218DC File Offset: 0x0001FADC
		public static NSString getLevelNameForPackLevel(int pack, int level)
		{
			return NSObject.NSS((pack + 1).ToString() + "_" + (level + 1).ToString() + ".xml");
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00021914 File Offset: 0x0001FB14
		public virtual void createPickerView()
		{
			View view = (View)new View().initFullscreen();
			RectangleElement rectangleElement = (RectangleElement)new RectangleElement().init();
			rectangleElement.color = RGBAColor.whiteRGBA;
			rectangleElement.width = (int)FrameworkTypes.SCREEN_WIDTH;
			rectangleElement.height = (int)FrameworkTypes.SCREEN_HEIGHT;
			view.addChild(rectangleElement);
			FontGeneric font = Application.getFont(6);
			Text text = new Text().initWithFont(font);
			text.setString(NSObject.NSS("START"));
			Text text2 = new Text().initWithFont(font);
			text2.setString(NSObject.NSS("START"));
			text2.scaleX = (text2.scaleY = 1.2f);
			Button button = new Button().initWithUpElementDownElementandID(text, text2, 0);
			button.anchor = (button.parentAnchor = 34);
			button.delegateButtonDelegate = this;
			view.addChild(button);
			this.addViewwithID(view, 0);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00021A04 File Offset: 0x0001FC04
		public override void activate()
		{
			base.activate();
			if (this.autoLoad)
			{
				NSString nsstring = NSObject.NSS("maps/" + this.selectedMap);
				XMLNode xmlnode = XMLNode.parseXML(nsstring.ToString());
				this.xmlLoaderFinishedWithfromwithSuccess(xmlnode, nsstring, xmlnode != null);
				return;
			}
			this.showView(0);
			this.loadList();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00021A5E File Offset: 0x0001FC5E
		public virtual void loadList()
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00021A60 File Offset: 0x0001FC60
		public override void deactivate()
		{
			base.deactivate();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00021A68 File Offset: 0x0001FC68
		public virtual void xmlLoaderFinishedWithfromwithSuccess(XMLNode rootNode, NSString url, bool success)
		{
			if (rootNode != null)
			{
				CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
				if (this.autoLoad)
				{
					CTRRootController.checkMapIsValid(ContentHelper.OpenResourceAsString(url.ToString()).ToCharArray());
				}
				ctrrootController.setMap(rootNode);
				ctrrootController.setMapName(this.selectedMap);
				ctrrootController.setMapsList(this.maplist);
				this.deactivate();
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		public virtual void setNormalMode()
		{
			this.autoLoad = false;
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			ctrrootController.setPicker(true);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		public virtual void setAutoLoadMap(NSString map)
		{
			this.autoLoad = true;
			CTRRootController ctrrootController = (CTRRootController)Application.sharedRootController();
			ctrrootController.setPicker(false);
			NSObject.NSREL(this.selectedMap);
			this.selectedMap = (NSString)NSObject.NSRET(map);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00021B34 File Offset: 0x0001FD34
		public virtual void onButtonPressed(int n)
		{
			if (n != 0)
			{
				return;
			}
			this.loadList();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00021B4E File Offset: 0x0001FD4E
		public override void dealloc()
		{
			NSObject.NSREL(this.selectedMap);
			base.dealloc();
		}

		// Token: 0x040009D8 RID: 2520
		private NSString selectedMap;

		// Token: 0x040009D9 RID: 2521
		private Dictionary<string, XMLNode> maplist;

		// Token: 0x040009DA RID: 2522
		private bool autoLoad;
	}
}
