namespace Models
{
	public static class ConvertProjects
	{
		public static Projects.ProjectJson ConvertProject(Projects.Project project)
		{
			Projects.ProjectJson projectJson = new Projects.ProjectJson(project);

			return projectJson;
		}

		public static Projects.Project ConvertProject(Projects.ProjectJson projectJson)
		{
			Projects.Project project = new Projects.Project(projectJson);

			return project;

		}
	}
}
