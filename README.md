# Blazorify.Bootstrap
Bootstrap-powered components for Blazor with runtime Sass compilation (via `Blazorify.Sass`) and zero external tooling.

## What you get
- Bootstrap-styled components (`BuiButton`, `BuiModal`, `BuiToast`, etc.).
- On-demand CSS per theme at `/_blazorify/bootstrap/{theme}.css` compiled with Dart Sass.
- A bundled helper script at `/_blazorify/bootstrap/scripts.js`.
- Light / dark / auto color modes: a no-flash startup script, `BuiThemeService`, `BuiThemeToggle`, and a `Theme` parameter on every component.
- Simple DI and middleware extensions to plug everything into your app.

## Quick start
Install the package:
```bash
dotnet add package Blazorify.Bootstrap
```

Add the namespace (e.g., `_Imports.razor`):
```razor
@using Blazorify.Bootstrap
```

Register services and middleware (`Program.cs`):
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorify(); // components + Sass + resources

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseBlazorify(); // exposes CSS + JS endpoints

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

Include the assets (e.g., in `_Host.cshtml` or `wwwroot/index.html`):
```html
<link rel="stylesheet" href="/_blazorify/bootstrap/bootstrap.css" />
<script src="/_blazorify/bootstrap/scripts.js"></script>
```

Use components:
```razor
<BuiButton Variant="Variant.Primary">Click me</BuiButton>
```

Toasts (container + service):
```razor
@inject BuiToastService Toasts

<BuiToastContainer Placement="Placement.TopEnd" />

<BuiButton Variant="Variant.Secondary" @onclick="Notify">Show toast</BuiButton>

@code {
	private Task Notify() => Toasts.Success("Saved!");
}
```

Modals (provider + service):
```razor
@inject BuiModalService Modals

<BuiModalProvider />

<BuiButton Variant="Variant.Primary" @onclick="OpenModal">Open modal</BuiButton>

@code {
	private Task OpenModal() => Modals.Show<SampleModal>(opts => {
		opts.Centered = true;
		opts.Size = Size.Large;
		opts.Data<SampleModal>(c => c.Message, "Hello from modal");
	});
}
```

`SampleModal.razor` (example modal content component):
```razor
@namespace YourApp.Components
@inherits BuiContentComponentBase

<BuiModalHeader>
	<h5 class="modal-title">Sample modal</h5>
</BuiModalHeader>

<BuiModalBody>
	<p>@Message</p>
</BuiModalBody>

<BuiModalFooter>
	<BuiButton Variant="Variant.Primary" @onclick="Close">Close</BuiButton>
</BuiModalFooter>

@code {
	[CascadingParameter] public BuiModal? Modal { get; set; }
	[Parameter] public String? Message { get; set; }

	private Task Close() => this.Modal?.Close() ?? Task.CompletedTask;
}
```

## Color modes (light / dark / auto)
Bootstrap 5.3 color modes are built in, modeled on the color mode toggler from the Bootstrap docs. The chosen mode is stored in `localStorage` (key `theme`) and applied as `data-bs-theme` on `<html>`; `auto` follows the operating system and updates live when it changes.

Load the startup script as a blocking script in `<head>`, before any stylesheet that should follow the mode, so the page is painted in the right mode without a flash:
```html
<script src="/_blazorify/bootstrap/color-mode.js"></script>
```
Without a stored choice the mode is `auto`; set another default with `data-default-mode="light"` (or `dark`) on that tag.

Drop in the toggle (a dropdown with Light / Dark / Auto, using [Bootstrap Icons](https://icons.getbootstrap.com/) names):
```razor
<BuiThemeToggle />
<BuiThemeToggle Dropdown="false" IncludeAuto="false" Variant="Variant.Link" /> @* single light/dark button *@
```
Parameters: `Dropdown`, `IncludeAuto`, `Variant`, `Size`, `Caret`, `MenuPlacement`, `Label` / `ShowLabel`, `LightLabel` / `DarkLabel` / `AutoLabel`, `LightIcon` / `DarkIcon` / `AutoIcon`, `OnChange`.

Or drive it from code with `BuiThemeService` (registered by `AddBlazorify()`; call it after the first render):
```razor
@inject BuiThemeService ThemeService

@code {
	protected override async Task OnAfterRenderAsync(bool firstRender) {
		if (firstRender) {
			var mode = await ThemeService.GetModeAsync();   // Light, Dark or Auto
			var shown = await ThemeService.GetThemeAsync(); // Light or Dark, with Auto resolved
		}
	}

	private Task UseDark() => ThemeService.SetModeAsync(ColorMode.Dark);
}
```
`ThemeService.Changed` fires when the mode changes, including from another browser tab.

Every component also takes a `Theme` parameter for a color mode on just that part of the page (rendered as `data-bs-theme`):
```razor
<BuiNavbar Theme="ColorMode.Dark">...</BuiNavbar>
<BuiDropdownMenu Theme="ColorMode.Dark">...</BuiDropdownMenu>
```

## Custom themes
Themes are compiled from embedded `.scss` files. The default `bootstrap` theme ships in this package with an `index.scss` entry point under `Blazorify.Bootstrap.Resources`.

Register your own theme (SCSS embedded in another assembly):
```csharp
builder.Services.AddBlazorify(options => {
	options.Themes["mytheme"] = new() {
		Assembly = typeof(MyThemeMarker).Assembly, // any type from your theme assembly
		Namespace = "MyCompany.MyTheme.Resources"  // prefix for embedded SCSS resources
	};
});
```

Request it at:
```html
<link rel="stylesheet" href="/_blazorify/bootstrap/mytheme.css" />
```

> Each theme needs an `index.scss` entry under the provided namespace. All `.scss` resources are extracted to a temp folder and compiled with Dart Sass (powered by the `Blazorify.Sass` package).

## Scripts endpoint
`/_blazorify/bootstrap/scripts.js` delivers helper JS for certain components (modals, file drop, etc.). Include it once per page before `</body>`.

## Contributing
Issues and PRs are welcome. Please keep changes focused and describe the scenario your contribution improves.
