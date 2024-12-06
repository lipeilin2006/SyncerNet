﻿using UniGLTF;
using UnityEngine;

namespace UniVRM10
{
    public class BuiltInVrm10MaterialExporter : IMaterialExporter
    {
        public BuiltInGltfMaterialExporter GltfExporter { get; set; } = new();
        public BuiltInVrm10MToonMaterialExporter MToonExporter { get; set; } = new();

        public glTFMaterial ExportMaterial(Material m, ITextureExporter textureExporter, GltfExportSettings settings)
        {
            if (MToonExporter.TryExportMaterial(m, textureExporter, out var dst))
            {
                return dst;
            }
            else
            {
                return GltfExporter.ExportMaterial(m, textureExporter, settings);
            }
        }
    }
}