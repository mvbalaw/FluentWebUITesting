This is a fluent wrapper around WatiN for web interface tests.

## Examples

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

	
## License		

[MIT License][mitlicense]

This project is part of [MVBA Law Commons][mvbalawcommons].

[mvbalawcommons]: http://code.google.com/p/mvbalaw-commons/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php

