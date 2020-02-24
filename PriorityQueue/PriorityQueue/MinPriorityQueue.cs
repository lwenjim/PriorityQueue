using System.Linq;

namespace ConsoleApplication1
{
    public class MinPriorityQueue<TNode> where TNode : IHeapValue
    {
        private TNode[] array;
        private int cursor;

        public MinPriorityQueue()
        {
            array = new TNode[32];
            cursor = 0;
        }

        public bool IsEmpty { get { return cursor <= 0; } }

        public void Insert(TNode x)
        {
            if (cursor + 1 == array.Count())
            {
                TNode[] tmparray = new TNode[array.Count() * 2];
                array.CopyTo(tmparray, 0);
                array = tmparray;
            }
            int xIndex = cursor++;
            array[xIndex] = x;
            while (xIndex / 2 >= 0)
            {
                if (array[xIndex / 2].Value <= array[xIndex].Value) break;
                TNode tmpNode = array[xIndex / 2];
                array[xIndex / 2] = array[xIndex];
                array[xIndex] = tmpNode;
                xIndex = xIndex / 2;
            }
        }

        public TNode Minimum()
        {
            return array[0];
        }

        public TNode ExtractMin()
        {
            TNode hn = array[0];
            array[0] = array[cursor - 1];
            array[cursor - 1] = default(TNode);
            cursor--;
            minHeapify(0, cursor - 1, 0);
            return hn;
        }

        private void minHeapify(int startIndex, int endIndex, int newRootIndex)
        {
            int L = (newRootIndex - startIndex + 1) * 2 + startIndex - 1; 
            int R = L + 1;
            int tmpLargestIndex = newRootIndex;
            if (L <= endIndex && array[L].Value.CompareTo(array[tmpLargestIndex].Value) < 0)
            {
                tmpLargestIndex = L;
            }
            if (R <= endIndex && array[R].Value.CompareTo(array[tmpLargestIndex].Value) < 0)
            {
                tmpLargestIndex = R;
            }
            if (tmpLargestIndex != newRootIndex)
            {
                TNode tmpT = array[tmpLargestIndex];
                array[tmpLargestIndex] = array[newRootIndex];
                array[newRootIndex] = tmpT;
                minHeapify(startIndex, endIndex, tmpLargestIndex);
            }

        }

        public void DecreaseKey(int nodeIndex, int newValue)
        {
            if (nodeIndex < cursor && array[nodeIndex].Value < newValue)
            {
                array[nodeIndex].Value = newValue;
                while (nodeIndex / 2 >= 0)
                {
                    if (array[nodeIndex / 2].Value <= array[nodeIndex].Value) break;
                    TNode tmpNode = array[nodeIndex / 2];
                    array[nodeIndex / 2] = array[nodeIndex];
                    array[nodeIndex] = tmpNode;
                    nodeIndex = nodeIndex / 2;
                }
            }
        }
    }
}
