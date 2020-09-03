FluentWebUITesting ReadMe
===
### Description

FluentWebUITesting is a fluent wrapper around WebDriver for web interface tests.

### Examples

	[Test]
	public void Should_display_the_date_the_installation_expires()
	{
		var steps = UITestRunner.InitializeWorkFlowContainer(
			b => b.LabelWithId("lblAccuMail").Verify(
					x => x.Exists().ShouldBeTrue(),
					x => x.Text().StartsWith("Expires on").ShouldBeTrue()
					)
			);
		RunTest("Public/Environment.aspx", "Environment", steps);
	}

	[Test]
	public void Should_navigate_to_the_Edit_page_if_Add_New_is_clicked()
	{
		var steps = UITestRunner.InitializeWorkFlowContainer(
			b => b.ButtonWithVisibleText("Add New").Click(),
			b => b.Title.ShouldBeEqualTo(EditTitle, "clicking Add New should put the user on the edit page"),
			b => b.ButtonWithVisibleText("Save").Exists().ShouldBeTrue(),
			b => b.ButtonWithVisibleText("Delete").Exists().ShouldBeFalse(),
			b => b.ButtonWithVisibleText("Download").Exists().ShouldBeFalse()
			);
		RunTest(PartialUrl, ListTitle, steps);
	}

### How To Build:

The build script requires Ruby with rake installed.

1. Run `InstallGems.bat` to get the ruby dependencies (only needs to be run once per computer)
1. open a command prompt to the root folder and type `rake` to execute rakefile.rb

If you do not have ruby:

1. open src\FluentWebUITesting.sln with Visual Studio and build the solution

### License

[MIT License][mitlicense]

This project is part of [MVBA's Open Source Projects][MvbaLawGithub].

If you have questions or comments about this project, please contact us at <mailto:opensource@mvbalaw.com>.

[MvbaLawGithub]: http://mvbalaw.github.io/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php
