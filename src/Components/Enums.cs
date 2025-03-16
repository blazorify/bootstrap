using System.ComponentModel.DataAnnotations;

namespace Blazorify.Bootstrap {
	public enum Justify {
		Start = 1,
		End = 2,
		Center = 4,
		Between = 8,
		Around = 16,
		Evenly = 32,
	}

	public enum Align {
		Default = 0,
		Baseline = 1,
		Top = 2,
		Middle = 4,
		Bottom = 8,
		TextTop = 16,
		TextBottom = 32,
	}

	public enum Placement {
		Start = 1,
		Top = 2,
		Bottom = 4,
		End = 8,
		Center = 16,
		Middle = 32,

		TopStart = Top | Start,
		TopCenter = Top | Center,
		TopEnd = Top | End,

		MiddleStart = Middle | Start,
		MiddleCenter = Middle | Center,
		MiddleEnd = Middle | End,

		BottomStart = Bottom | Start,
		BottomCenter = Bottom | Center,
		BottomEnd = Bottom | End,
	}

	public enum Variant {
		None = 0,
		Primary = 1,
		Secondary = 2,
		Success = 4,
		Danger = 8,
		Warning = 16,
		Info = 32,
		Light = 64,
		Dark = 128,
		Link = 256,
	}

	public enum Size {
		Normal = 1,
		Small = 2,
		Large = 4,
		ExtraLarge = 8,
	}

	public enum TextDecoration {
		Unset = 0,
		None = 1,
		Underline = 2,
		LineThrough = 4,
	}

	public enum Border {
		None = 0,

		Top = 1,
		End = 2,
		Bottom = 4,
		Start = 8,

		All = Top | End | Bottom | Start,
	}

	public enum Rounded {
		None = 0,
		Top = 1,
		Bottom = 2,
		Start = 4,
		End = 8,
		Pill = 16,
		Circle = 32,
		All = 64,
	}

	public enum ButtonType {
		Button = 1,
		Submit = 2,
		Reset = 4,
	}

	public enum AutoClose {
		ClickInside = 1,
		ClickOutside = 2,
		Manually = 4,
	}

	public enum NavbarExpand {
		Always = 1,
		SM = 2,
		MD = 4,
		LG = 8,
		XL = 16,
		XXL = 32,
	}

	public enum NavType {
		None = 0,
		Tabs = 1,
		Pills = 2,
		Underline = 4,
	}
}
