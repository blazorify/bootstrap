# Blazorify.Bootstrap
Bootstrap-powered components for Blazor with runtime Sass compilation (via `Blazorify.Sass`) and zero external tooling.

## What you get
- Bootstrap-styled components (`BuiButton`, `BuiModal`, `BuiToast`, etc.).
- On-demand CSS per theme at `/_blazorify/bootstrap/{theme}.css` compiled with Dart Sass.
- A bundled helper script at `/_blazorify/bootstrap/scripts.js`.
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
