using System;

using ctr_wp7.iframework.core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ctr_wp7.wp7utilities
{
    // Token: 0x020000FF RID: 255
    public class Camera2d
    {
        // Token: 0x060007AF RID: 1967 RVA: 0x0003C408 File Offset: 0x0003A608
        public Camera2d(Vector2 position)
        {
            this.zoom_ = new Vector2(1f, 1f);
            this.rotation_ = 0f;
            this.translate_ = position;
            this.positionZero_ = position;
            this.move_ = default(Vector2);
            this.rotationCenter_ = position;
        }

        // Token: 0x17000022 RID: 34
        // (get) Token: 0x060007B0 RID: 1968 RVA: 0x0003C45C File Offset: 0x0003A65C
        // (set) Token: 0x060007B1 RID: 1969 RVA: 0x0003C464 File Offset: 0x0003A664
        public Vector2 Zoom
        {
            get
            {
                return this.zoom_;
            }
            set
            {
                this.zoom_ = value;
            }
        }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x060007B2 RID: 1970 RVA: 0x0003C46D File Offset: 0x0003A66D
        // (set) Token: 0x060007B3 RID: 1971 RVA: 0x0003C475 File Offset: 0x0003A675
        public float Rotation
        {
            get
            {
                return this.rotation_;
            }
            set
            {
                this.rotation_ = value;
            }
        }

        // Token: 0x060007B4 RID: 1972 RVA: 0x0003C47E File Offset: 0x0003A67E
        public void Move(Vector2 amount)
        {
            this.move_ -= amount;
        }

        // Token: 0x17000024 RID: 36
        // (get) Token: 0x060007B5 RID: 1973 RVA: 0x0003C492 File Offset: 0x0003A692
        // (set) Token: 0x060007B6 RID: 1974 RVA: 0x0003C49A File Offset: 0x0003A69A
        public Vector2 Translate
        {
            get
            {
                return this.translate_;
            }
            set
            {
                this.translate_ = value * -1f;
                this.translate_ += this.positionZero_;
            }
        }

        // Token: 0x17000025 RID: 37
        // (get) Token: 0x060007B7 RID: 1975 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
        // (set) Token: 0x060007B8 RID: 1976 RVA: 0x0003C4CC File Offset: 0x0003A6CC
        public Vector2 RotationCenter
        {
            get
            {
                return this.rotationCenter_;
            }
            set
            {
                this.rotationCenter_ = value;
            }
        }

        // Token: 0x17000026 RID: 38
        // (get) Token: 0x060007B9 RID: 1977 RVA: 0x0003C4D5 File Offset: 0x0003A6D5
        public Vector Center
        {
            get
            {
                return new Vector((float)ScreenSizes.Width2 - this.move_.X, (float)ScreenSizes.Height2 - this.move_.Y);
            }
        }

        // Token: 0x060007BA RID: 1978 RVA: 0x0003C500 File Offset: 0x0003A700
        public Matrix getTransformation(GraphicsDevice graphicsDevice)
        {
            this.transform_ = Matrix.CreateTranslation(new Vector3(-this.translate_.X + this.move_.X, -this.translate_.Y + this.move_.Y, 0f)) * Matrix.CreateRotationZ(this.Rotation) * Matrix.CreateScale(new Vector3(this.Zoom.X, this.Zoom.Y, 1f)) * Matrix.CreateTranslation(new Vector3((float)graphicsDevice.Viewport.Width * 0.5f, (float)graphicsDevice.Viewport.Height * 0.5f, 0f));
            return this.transform_;
        }

        // Token: 0x04000D07 RID: 3335
        private Vector2 zoom_;

        // Token: 0x04000D08 RID: 3336
        private Matrix transform_;

        // Token: 0x04000D09 RID: 3337
        private Vector2 translate_;

        // Token: 0x04000D0A RID: 3338
        private Vector2 move_;

        // Token: 0x04000D0B RID: 3339
        private Vector2 positionZero_;

        // Token: 0x04000D0C RID: 3340
        private float rotation_;

        // Token: 0x04000D0D RID: 3341
        private Vector2 rotationCenter_;
    }
}
