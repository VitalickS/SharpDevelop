// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using ICSharpCode.AvalonEdit.Document;
using System;
using System.Drawing;
using ICSharpCode.SharpDevelop.Bookmarks;

namespace ICSharpCode.SharpDevelop.Debugging
{
	public enum BreakpointAction {
		Break, Trace, Condition
	}
	
	public class BreakpointBookmark : SDMarkerBookmark
	{
		bool isHealthy = true;
		bool isEnabled = true;
		string tooltip;
		
		static readonly Color defaultColor = Color.FromArgb(180, 38, 38);
		
		BreakpointAction action = BreakpointAction.Break;
		string condition;
		string scriptLanguage;
		
		public string ScriptLanguage {
			get { return scriptLanguage; }
			set { scriptLanguage = value; }
		}
		
		public string Condition {
			get { return condition; }
			set { condition = value; }
		}
		
		public BreakpointAction Action {
			get { return action; }
			set { this.action = value; }
		}
		
		public virtual bool IsHealthy {
			get {
				return isHealthy;
			}
			set {
				if (isHealthy != value) {
					isHealthy = value;
					Redraw();
				}
			}
		}
		
		public virtual bool IsEnabled {
			get {
				return isEnabled;
			}
			set {
				if (isEnabled != value) {
					isEnabled = value;
					if (IsEnabledChanged != null)
						IsEnabledChanged(this, EventArgs.Empty);
					Redraw();
				}
			}
		}
		
		public event EventHandler IsEnabledChanged;
		
		public string Tooltip {
			get { return tooltip; }
			set { tooltip = value; }
		}
		
		public BreakpointBookmark(string fileName, TextLocation location, BreakpointAction action, string scriptLanguage, string script) : base(fileName, location)
		{
			this.action = action;
			this.scriptLanguage = scriptLanguage;
			this.condition = script;
		}
		
		public static readonly IImage BreakpointImage = new ResourceServiceImage("Bookmarks.Breakpoint");
		public static readonly IImage DisabledBreakpointImage = new ResourceServiceImage("Bookmarks.DisabledBreakpoint");
		public static readonly IImage UnhealthyBreakpointImage = new ResourceServiceImage("Bookmarks.UnhealthyBreakpoint");
		
		public override IImage Image {
			get {
				if (!this.IsEnabled)
					return DisabledBreakpointImage;
				else if (this.IsHealthy)
					return BreakpointImage;
				else
					return UnhealthyBreakpointImage;
			}
		}
		
		/*
		protected override TextMarker CreateMarker()
		{
			LineSegment lineSeg = Anchor.Line;
			TextMarker marker = new TextMarker(lineSeg.Offset, lineSeg.Length, TextMarkerType.SolidBlock, defaultColor, Color.White);
			Document.MarkerStrategy.AddMarker(marker);
			return marker;
		}*/
	}
}
