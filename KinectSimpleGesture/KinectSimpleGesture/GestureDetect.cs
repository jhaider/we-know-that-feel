using System;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KinectSimpleGesture
{
    public enum Mode { IDLE, RECORD, DETECT };

	public class GestureDetect
	{
		readonly int WINDOW_SIZE = 500;
		TrieNode m_currentNode = null;
		int m_segmentCount = 0;
		int m_frameCount = 0;
        TrieNode m_root = null;
        string m_detectedGesture = "";
        List<GestureSegment> recordedSegments = new List<GestureSegment>();

		public event EventHandler GestureRecognized;

		public GestureDetect () {
            m_currentNode = new TrieNode();
            List<GestureSegment> segments = new List<GestureSegment>();

            Dictionary<JointType, JointData> dict1 = new Dictionary<JointType,JointData>();
           
            JointData data = new JointData(JointType.ElbowLeft, -4.50156369656848, 76.316932955313, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.51893426700379, -85.4183328123706, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.27420548570973, 79.0775137239691, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.79117827175244, -83.7977168089263, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.50458482122497, 58.5640350015726, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.29933884339533, -84.7637953603602, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.09018793140734, 76.7740072969837, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.32410390623434, -81.9923424537729, 10);
            dict1.Add(JointType.WristRight, data);
            GestureSegment segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -2.65120821954993, 80.9836459608154, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.51039503051607, -85.449379610491, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -4.33679713468812, 80.0299024459167, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.79303614652534, -83.8031257412415, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.30808671510396, 58.295435701722, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.2797716225607, -84.8420419754879, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -5.11624594527998, 77.6154861050017, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.32196919837735, -82.0158057631026, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.52696138753418, 76.050446116649, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.57481632473222, -85.2386427383755, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.29344972555013, 78.8845237995042, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.78430269993827, -83.8351120537675, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -7.73010110339442, 59.4834490383893, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.29915281642949, -84.7414405710047, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.09391277772654, 76.584821792357, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.30937029873276, -82.0683823755423, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.55997368322977, 76.0492472976981, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.76899733093313, -84.6415797924758, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.50142780873238, 78.5007140684528, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -2.58818375299741, 83.8235541601875, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.70737767416432, 58.4645819026804, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.03908323631209, -85.7812872564111, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.29745722682813, 76.209996863806, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, -2.03893538810967, 84.6539053339944, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.76788888422445, 77.9908126242001, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 0.170834344326128, -89.4926318580757, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -12.7602854702621, 56.4400742384674, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -3.66677897947356, 81.995017948452, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.85565048977197, 57.9722900347853, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.500513295503658, -87.9057823779831, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.5800344301698, 60.7816349446109, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, -2.63102505581758, 83.8019954092871, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -5.75231350290312, 66.9553417984875, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 4.53692253537415, -67.9569673830934, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -12.7146546412539, 52.6287162529574, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -1.36900750114951, 78.5402686537019, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -12.171888257629, 51.2432022633995, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.42775363040743, -83.9996583503326, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.9645051323317, 55.3888097856532, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 0.0419401254424694, -89.6932169970724, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.99502679796075, 72.6206450326225, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 5.66616058838355, -73.1782113477185, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -7.98342558631133, 68.4934529522154, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 12.295566468161, -45.0739402511015, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.73430475576969, 57.4977914558669, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.672128830331295, -87.2719292062135, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.90371844921663, 69.2693206614554, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 10.6331117539806, -52.7667536470163, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.6795045384253, 73.5843093387949, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 7.20321681872982, -65.7034055466752, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -7.57031458883527, 69.4232084190014, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 12.3539041182334, -33.8672038076763, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.40002768889213, 58.130452149117, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.878135081983395, -86.4535620095332, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.51918867469597, 70.1931973685357, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 11.0712796749129, -42.7996757536621, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -6.15307368845637, 64.7615235705074, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 5.69418170135753, -61.023306612055, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -11.1796353833734, 53.0192798243889, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -0.310151351797389, 87.7954266795302, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -4.24973249211829, 61.2639998515406, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 2.15197412927674, -64.7660502959084, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.3525787518531, 56.7449018452825, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 0.882833195831113, -82.4209887968226, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.94041132093647, 73.5463516374905, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 7.15939420641997, -65.4584753854321, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -9.11171717555313, 67.3790769319718, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 15.3477712244698, -44.1046220794308, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.21448431283458, 58.5988040447074, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.05695666236123, -85.7238423580136, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -8.00818904719426, 68.6883867901354, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 13.1789632828376, -48.7924370113171, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            m_currentNode.addSegments(segments, "Wave");
            m_root = m_currentNode;

        }

	    public void TestGestureDetect () {

	        List<GestureSegment> segments = new List<GestureSegment>();

            Dictionary<JointType, JointData> dict1 = new Dictionary<JointType, JointData>();

            JointData data = new JointData(JointType.ElbowLeft, -4.50156369656848, 76.316932955313, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.51893426700379, -85.4183328123706, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.27420548570973, 79.0775137239691, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.79117827175244, -83.7977168089263, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.50458482122497, 58.5640350015726, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.29933884339533, -84.7637953603602, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.09018793140734, 76.7740072969837, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.32410390623434, -81.9923424537729, 10);
            dict1.Add(JointType.WristRight, data);
            GestureSegment segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -2.65120821954993, 80.9836459608154, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.51039503051607, -85.449379610491, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -4.33679713468812, 80.0299024459167, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.79303614652534, -83.8031257412415, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.30808671510396, 58.295435701722, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.2797716225607, -84.8420419754879, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -5.11624594527998, 77.6154861050017, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.32196919837735, -82.0158057631026, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.52696138753418, 76.050446116649, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.57481632473222, -85.2386427383755, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.29344972555013, 78.8845237995042, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 2.78430269993827, -83.8351120537675, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -7.73010110339442, 59.4834490383893, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.29915281642949, -84.7414405710047, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.09391277772654, 76.584821792357, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 3.30937029873276, -82.0683823755423, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.55997368322977, 76.0492472976981, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 1.76899733093313, -84.6415797924758, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -5.50142780873238, 78.5007140684528, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -2.58818375299741, 83.8235541601875, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.70737767416432, 58.4645819026804, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.03908323631209, -85.7812872564111, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.29745722682813, 76.209996863806, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, -2.03893538810967, 84.6539053339944, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.76788888422445, 77.9908126242001, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 0.170834344326128, -89.4926318580757, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -12.7602854702621, 56.4400742384674, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -3.66677897947356, 81.995017948452, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.85565048977197, 57.9722900347853, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.500513295503658, -87.9057823779831, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.5800344301698, 60.7816349446109, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, -2.63102505581758, 83.8019954092871, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -5.75231350290312, 66.9553417984875, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 4.53692253537415, -67.9569673830934, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -12.7146546412539, 52.6287162529574, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -1.36900750114951, 78.5402686537019, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -12.171888257629, 51.2432022633995, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.42775363040743, -83.9996583503326, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.9645051323317, 55.3888097856532, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 0.0419401254424694, -89.6932169970724, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.99502679796075, 72.6206450326225, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 5.66616058838355, -73.1782113477185, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -7.98342558631133, 68.4934529522154, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 12.295566468161, -45.0739402511015, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.73430475576969, 57.4977914558669, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.672128830331295, -87.2719292062135, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.90371844921663, 69.2693206614554, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 10.6331117539806, -52.7667536470163, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -3.6795045384253, 73.5843093387949, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 7.20321681872982, -65.7034055466752, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -7.57031458883527, 69.4232084190014, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 12.3539041182334, -33.8672038076763, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.40002768889213, 58.130452149117, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 0.878135081983395, -86.4535620095332, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -6.51918867469597, 70.1931973685357, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 11.0712796749129, -42.7996757536621, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -6.15307368845637, 64.7615235705074, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 5.69418170135753, -61.023306612055, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -11.1796353833734, 53.0192798243889, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, -0.310151351797389, 87.7954266795302, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -4.24973249211829, 61.2639998515406, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 2.15197412927674, -64.7660502959084, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -10.3525787518531, 56.7449018452825, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 0.882833195831113, -82.4209887968226, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);
            dict1.Clear();

            data = new JointData(JointType.ElbowLeft, -4.94041132093647, 73.5463516374905, 10);
            dict1.Add(JointType.ElbowLeft, data);
            data = new JointData(JointType.ElbowRight, 7.15939420641997, -65.4584753854321, 10);
            dict1.Add(JointType.ElbowRight, data);
            data = new JointData(JointType.HandLeft, -9.11171717555313, 67.3790769319718, 10);
            dict1.Add(JointType.HandLeft, data);
            data = new JointData(JointType.HandRight, 15.3477712244698, -44.1046220794308, 10);
            dict1.Add(JointType.HandRight, data);
            data = new JointData(JointType.ShoulderLeft, -8.21448431283458, 58.5988040447074, 10);
            dict1.Add(JointType.ShoulderLeft, data);
            data = new JointData(JointType.ShoulderRight, 1.05695666236123, -85.7238423580136, 10);
            dict1.Add(JointType.ShoulderRight, data);
            data = new JointData(JointType.WristLeft, -8.00818904719426, 68.6883867901354, 10);
            dict1.Add(JointType.WristLeft, data);
            data = new JointData(JointType.WristRight, 13.1789632828376, -48.7924370113171, 10);
            dict1.Add(JointType.WristRight, data);
            segment = new GestureSegment(dict1);
            segments.Add(segment);

            string gestureName = m_currentNode.isGesture(segments);
	        Console.Write("detected: " + gestureName);

	    }

        public GestureDetect(TrieNode trieNode) {
            m_currentNode = trieNode;
        }

		/// <summary>
		/// Updates the current gesture.
		/// </summary>
		/// <param name="skeleton">The skeleton data.</param>
		public void Update(Skeleton skeleton, Mode mode) {
			// Create a gesture segment with the deltas compared to the axis
            // Get the difference from the current node to the generated gesture segment

            //Console.Write("Update");

            GestureSegment segment = GestureSegment.generateSegmentFromSkeleton(skeleton);
            //Console.Write("Update here ");

            switch (mode)
            {
                case Mode.DETECT:
                    DetectGesture(segment);
                    break;
                case Mode.RECORD:
                    RecordGesture(segment);
                    break;
                case Mode.IDLE:
                    //Console.WriteLine("should not happen: idle in Update")
                    break;

            }
		}

        public string GetGestureName()
        {
            return m_detectedGesture;
        }

        public void RecordGesture(GestureSegment segment)
        {
            recordedSegments.Add(segment);
        }

        public void Reset()
        {
            recordedSegments.Clear();
            m_currentNode = m_root;
            m_detectedGesture = "";
        }

        public void StopRecording(string gestureName)
        {
            Console.WriteLine("children b:" + m_currentNode.m_children.Count);
            if (gestureName.Length > 0)
            {
                aggregateSegments(gestureName);
            }
           Console.WriteLine("children a:" + m_currentNode.m_children.Count);

            Reset();
        }

        private void aggregateSegments(String gestureName)
        {
            int xDirection = 0;
            int yDirection = 0;
            GestureSegment currentSegment = null;

            List<GestureSegment> aggregatedSegments = new List<GestureSegment>();

            // Parse through each segment in the recorded segments
            foreach (GestureSegment segment in recordedSegments) {
                if (currentSegment == null) {
                    aggregatedSegments.Add(segment);
                    currentSegment = segment;
                    continue;
                }

                // Check for each joint to see if they are in range
                foreach (KeyValuePair<JointType, JointData> joint in segment.Joints) {
				    JointData data;
                    if (currentSegment.Joints.TryGetValue(joint.Key, out data)) {
                        int xDir = data.calculateDirection(joint.Value, JointData.Axis.x);
                        int yDir = data.calculateDirection(joint.Value, JointData.Axis.y);

                        if (xDirection == 0) {
                            xDirection = xDir;
                        }

                        if (yDirection == 0) {
                            yDirection = yDir;
                        }

                        if (!data.InAggregate(joint.Value) || xDir != xDirection || yDir != yDirection) {
                            aggregatedSegments.Add(segment);
                            currentSegment = segment;
                            xDirection = xDir;
                            yDirection = yDir;
                            break;
                        }
                    }

                }

            }

            // Calculate the difference between each angle, if the the angles are heading the same direction AND in the current group range, then discard
            // Else add it to aggregateSegments
           
            m_currentNode.addSegments(aggregatedSegments, gestureName);

        }

        public void StopDetecting()
        {
            Reset();
        }

		public bool DetectGesture(GestureSegment segment)
		{

            //Console.Write("RAWR");
			// Check the trie to see if the segment is found
			// Null is returned if nothing is found
			TrieNode result = m_currentNode.findChild(segment);
            //Console.Write("boo");
			if (result != null) {
				if (result.isTerminal) {
                    if (GestureRecognized != null) {
                        GestureRecognized(this, new GestureEventArgs());
                    }
                    Console.WriteLine("DETECTED: " + m_detectedGesture);
                    m_detectedGesture = result.getName();
					m_segmentCount = 0;
					m_frameCount = 0;
					return true;
				} else {
                    Console.WriteLine("Matched a segment!" + result.id);
					m_segmentCount++;
					m_frameCount = 0;
				}
                m_currentNode = result;
			} else if (m_frameCount == WINDOW_SIZE){
				m_segmentCount = 0;
				m_frameCount = 0;
			} else {
				m_frameCount++;
			}

			return false;
		}


	}

	class GestureEventArgs : EventArgs {
		TrieNode m_gesture;
        public GestureEventArgs() {}
        public GestureEventArgs (TrieNode gesture) {
			m_gesture = gesture;
		}
	}

}

