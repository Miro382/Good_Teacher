using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Good_Teacher.Class.Serialization
{
    public class Effect_Serializer
    {

        public Color color;
        public double direction, shadowDepth, ShadowblurRadius,BlurRadius;
        public double opacity;
        public KernelType kernelType;
        public bool IsShadow = false;


        public Effect_Serializer()
        {

        }

        public Effect_Serializer(Effect effect)
        {
            if (effect is DropShadowEffect)
                SerializeShadow((DropShadowEffect)effect);
            else if (effect is BlurEffect)
                SerializeBlur((BlurEffect)effect);
        }

        public Effect_Serializer(BlurEffect BLeffect)
        {
                SerializeBlur(BLeffect);
        }


        public Effect_Serializer(DropShadowEffect SHeffect)
        {
                SerializeShadow(SHeffect);
        }

        /// <summary>
        /// Create automatic effect from values
        /// </summary>
        /// <returns>Effect</returns>
        public Effect CreateEffect()
        {
            if (IsShadow)
            {
                return new DropShadowEffect
                {
                    Color = color,
                    Direction = direction,
                    ShadowDepth = shadowDepth,
                    Opacity = opacity,
                    BlurRadius = ShadowblurRadius,
                    RenderingBias = RenderingBias.Performance
                };
            }
            else
            {
                return new BlurEffect
                {
                    KernelType = kernelType,
                    Radius = BlurRadius,
                    RenderingBias = RenderingBias.Performance
                };
            }
        }

        /// <summary>
        /// Create Drop shadow effect
        /// </summary>
        /// <returns>DropShadowEffect</returns>
        public DropShadowEffect CreateShadow()
        {
            return new DropShadowEffect
            {
                Color = color,
                Direction = direction,
                ShadowDepth = shadowDepth,
                Opacity = opacity,
                BlurRadius = ShadowblurRadius,
                RenderingBias = RenderingBias.Performance
            };
        }

        /// <summary>
        /// Create Blur effect
        /// </summary>
        /// <returns>BlurEffect</returns>
        public BlurEffect CreateBlur()
        {
            return new BlurEffect
            {
                KernelType = kernelType,
                Radius = BlurRadius,
                RenderingBias = RenderingBias.Performance
            };
        }


        public void Serialize(Effect effect)
        {
            if (effect is DropShadowEffect)
            {
                SerializeShadow((DropShadowEffect)effect);

            }else if(effect is BlurEffect)
            {
                SerializeBlur((BlurEffect)effect);
            }
        }


        public void SerializeShadow(DropShadowEffect effect)
        {
            color = effect.Color;
            direction = effect.Direction;
            shadowDepth = effect.ShadowDepth;
            opacity = effect.Opacity;
            ShadowblurRadius = effect.BlurRadius;
            IsShadow = true;
        }


        public void SerializeBlur(BlurEffect effect)
        {
            kernelType = effect.KernelType;
            BlurRadius = effect.Radius;
            IsShadow = false;
        }

    }

}
