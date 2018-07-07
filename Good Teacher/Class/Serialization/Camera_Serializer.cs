using System.Windows.Media.Media3D;

namespace Good_Teacher.Class.Serialization
{
    public class Camera_Serializer
    {
        public Point3D position = new Point3D(0,0,0);
        public Vector3D lookdirection = new Vector3D(0,0,1);
        public Vector3D updirection = new Vector3D(0,0,0);

        public Camera_Serializer()
        {

        }

        public Camera_Serializer(ProjectionCamera pcamera)
        {
            Serialize(pcamera);
        }


        public void Serialize(ProjectionCamera camera)
        {
            position = camera.Position;
            lookdirection = camera.LookDirection;
            updirection = camera.UpDirection;
        }

        public void Deserialize(ProjectionCamera camera)
        {
            camera.Position = position;
            camera.LookDirection = lookdirection;
            camera.UpDirection = updirection;
        }

    }
}
