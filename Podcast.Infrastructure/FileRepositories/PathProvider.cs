using Microsoft.AspNetCore.Hosting;
using Podcast.Domain;
using Podcast.Domain.Equipe;
using System;
using System.IO;

namespace Podcast.Infrastructure.FileRepositories
{
    public sealed class PathProvider : IPathProvider
    {
        private readonly string workingPath;
        public PathProvider(IHostingEnvironment hostingEnvironment)
        {
            workingPath = Path.Combine(hostingEnvironment.WebRootPath, "Content");
            if (!Directory.Exists(workingPath))
                Directory.CreateDirectory(workingPath);
        }

        private string SanitizeName(string name)
        {
            var invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(Path.GetInvalidFileNameChars()) + " ");
            var invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }

        public string GetSaveFileName(Enseignant enseignant) => Path.Combine(workingPath, $"{SanitizeName($"{enseignant}")}.save");

        public string GetTeamFileName() => Path.Combine(workingPath, "team.save");

        public string GetAccountsFileName() => Path.Combine(workingPath, "accounts.save");

        public string GetTeacherBrowsingFolder(Enseignant enseignant) => $"Content/{SanitizeName($"{enseignant}")}";
        public string GetTeacherSaveFolder(Enseignant enseignant)
        {
            var path = Path.Combine(workingPath, SanitizeName($"{enseignant}"));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

    }
}