using ctr_wp7.iframework.core;

namespace ctr_wp7.iframework.visual
{
    internal class MultiParticles : Particles
    {
        public virtual Particles initWithTotalParticlesandImageGrid(int numberOfParticles, Image image)
        {
            if (base.init() == null)
            {
                return null;
            }
            imageGrid = image;
            drawer = new ImageMultiDrawer().initWithImageandCapacity(imageGrid, numberOfParticles);
            width = (int)SCREEN_WIDTH;
            height = (int)SCREEN_HEIGHT;
            totalParticles = numberOfParticles;
            particles = new Particle[totalParticles];
            colors = new RGBAColor[4 * totalParticles];
            if (particles == null || colors == null)
            {
                particles = null;
                colors = null;
                return null;
            }
            active = false;
            blendAdditive = false;
            OpenGL.glGenBuffers(1, ref colorsID);
            return this;
        }

        public override void initParticle(ref Particle particle)
        {
            Image image = imageGrid;
            int num = RND(image.texture.quadsCount - 1);
            Quad2D quad2D = image.texture.quads[num];
            Quad3D quad3D = Quad3D.MakeQuad3D(0f, 0f, 0f, 0f, 0f);
            Rectangle rectangle = image.texture.quadRects[num];
            drawer.setTextureQuadatVertexQuadatIndex(quad2D, quad3D, particleCount);
            base.initParticle(ref particle);
            particle.width = rectangle.w * particle.size;
            particle.height = rectangle.h * particle.size;
        }

        public override void updateParticle(ref Particle p, float delta)
        {
            if (p.life > 0f)
            {
                Vector vector = vectZero;
                if (p.pos.x != 0f || p.pos.y != 0f)
                {
                    vector = vectNormalize(p.pos);
                }
                Vector vector2 = vector;
                vector = vectMult(vector, p.radialAccel);
                float x = vector2.x;
                vector2.x = -vector2.y;
                vector2.y = x;
                vector2 = vectMult(vector2, p.tangentialAccel);
                Vector vector3 = vectAdd(vectAdd(vector, vector2), gravity);
                vector3 = vectMult(vector3, delta);
                p.dir = vectAdd(p.dir, vector3);
                vector3 = vectMult(p.dir, delta);
                p.pos = vectAdd(p.pos, vector3);
                p.color.r += p.deltaColor.r * delta;
                p.color.g += p.deltaColor.g * delta;
                p.color.b += p.deltaColor.b * delta;
                p.color.a += p.deltaColor.a * delta;
                p.life -= delta;
                drawer.vertices[particleIdx] = Quad3D.MakeQuad3D((double)(p.pos.x - (p.width / 2f)), (double)(p.pos.y - (p.height / 2f)), 0.0, p.width, p.height);
                for (int i = 0; i < 4; i++)
                {
                    colors[(particleIdx * 4) + i] = p.color;
                }
                particleIdx++;
                return;
            }
            if (particleIdx != particleCount - 1)
            {
                particles[particleIdx] = particles[particleCount - 1];
                drawer.vertices[particleIdx] = drawer.vertices[particleCount - 1];
                drawer.texCoordinates[particleIdx] = drawer.texCoordinates[particleCount - 1];
            }
            particleCount--;
        }

        public override void update(float delta)
        {
            base.update(delta);
            if (active && emissionRate != 0f)
            {
                float num = 1f / emissionRate;
                emitCounter += delta;
                while (particleCount < totalParticles && emitCounter > num)
                {
                    _ = addParticle();
                    emitCounter -= num;
                }
                elapsed += delta;
                if (duration != -1f && duration < elapsed)
                {
                    stopSystem();
                }
            }
            particleIdx = 0;
            while (particleIdx < particleCount)
            {
                updateParticle(ref particles[particleIdx], delta);
            }
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glBufferData(2, colors, 3);
            OpenGL.glBindBuffer(2, 0U);
        }

        public override void draw()
        {
            preDraw();
            if (blendAdditive)
            {
                OpenGL.glBlendFunc(BlendingFactor.GL_SRC_ALPHA, BlendingFactor.GL_ONE);
            }
            else
            {
                OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            }
            OpenGL.glEnable(0);
            OpenGL.glBindTexture(drawer.image.texture.name());
            OpenGL.glVertexPointer(3, 5, 0, toFloatArray(drawer.vertices));
            OpenGL.glTexCoordPointer(2, 5, 0, toFloatArray(drawer.texCoordinates));
            OpenGL.glEnableClientState(13);
            OpenGL.glBindBuffer(2, colorsID);
            OpenGL.glColorPointer(4, 5, 0, colors);
            OpenGL.glDrawElements(7, particleIdx * 6, drawer.indices);
            OpenGL.glBlendFunc(BlendingFactor.GL_ONE, BlendingFactor.GL_ONE_MINUS_SRC_ALPHA);
            OpenGL.glBindBuffer(2, 0U);
            OpenGL.glDisableClientState(13);
            postDraw();
        }

        public override void dealloc()
        {
            drawer = null;
            imageGrid = null;
            base.dealloc();
        }

        public ImageMultiDrawer drawer;

        public Image imageGrid;
    }
}
