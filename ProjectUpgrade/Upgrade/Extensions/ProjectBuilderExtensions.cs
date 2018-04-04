﻿using System.Collections.Generic;
using System.Linq;
using ProjectUpgrade.Extensions;
using ProjectUpgrade.Upgrade.Interfaces;
using ProjectUpgrade.Upgrade.Models;

namespace ProjectUpgrade.Upgrade.Extensions
{
    public static class ProjectBuilderExtensions
    {
        public static IProjectBuilder AddPackageDependencies(this IProjectBuilder builder,
                                                             IEnumerable<PackageDependencyModel> packageDependencies)
        {
            var groupBuilder = builder.AddItemGroup();
            foreach (var dependency in packageDependencies)
            {
                groupBuilder.WithElement("PackageReference")
                            .WithAttribute("Include", dependency.PackageId)
                            .WithAttribute("Version", dependency.Version);
            }

            return builder;
        }

        public static IProjectBuilder AddProjectReferences(this IProjectBuilder builder,
                                                           IEnumerable<ProjectReferenceModel> projectReferences)
        {
            var groupBuilder = builder.AddItemGroup();
            foreach (var reference in projectReferences)
            {
                groupBuilder.WithElement("ProjectReference")
                            .WithAttribute("Include", reference.RelativePath);
            }

            return builder;
        }

        public static IProjectBuilder SetProjectOptions(this IProjectBuilder builder,
                                                        OptionsModel options)
        {
            var pairs = options.ToNameValuePair().ToList();
            if (pairs.Any())
            {
                var groupBuilder = builder.AddPropertyGroup();
                foreach (var (name, value) in pairs)
                {
                    groupBuilder.WithElement(name)
                                .WithNodeValue(value);
                }
            }

            return builder;
        }
    }
}
