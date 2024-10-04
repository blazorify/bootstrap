using System.ComponentModel.DataAnnotations;

namespace Blazorify.Bootstrap {
	public enum Placement {
		Start = 1,
		Top = 2,
		Bottom = 4,
		End = 8,
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
	}

	public enum ButtonType {
		Button = 1,
		Submit = 2,
		Reset = 4,
	}

	public enum AutoClose {
		Always = 1,
		Inside = 2,
		Outside = 4,
		Manually = 8,
	}

	public enum NavbarExpand {
		Always = 1,
		SM = 2,
		MD = 4,
		LG = 8,
		XL = 16,
		XXL = 32,
	}
}
