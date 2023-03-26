using System;

namespace Stack_report
{
    internal class Program
    {
        class Node
        {
            public int key; // 링크드리스트의 데이터
            public Node next; // 다음을 가리키는 노드
            public Node(int _key)
            {
                key = _key;
                next = null;
            }
        }
        class LinkedList
        {
            private Node head; // 첫 번째 노드를 가리키는 head

            public void addHead(int key) // key를 갖는 노드를 만들어 리스트의 맨 앞에 추가
            {
                Node newnode = new Node(key);
                newnode.next = head;
                head = newnode;
            }
            public int getHead()
            {
                if (head == null) // 헤드가 비었으면 
                    return -1; // 함수 종료하고 호출했던 곳으로 되돌아감
                return head.key; // 헤드의 데이터 리턴
            }
            public void removeHead()
            {
                head = head.next; // 헤드 노드 삭제
            }
            public void addTail(int key) // key를 갖는 노드를 만들어 리스트의 맨 뒤에 추가
            {
                Node newnode = new Node(key);
                if (head == null)
                {
                    head = newnode;
                    return;
                }
                Node ptr = head;
                while (ptr.next != null) // 노드의 끝에 도달
                {
                    ptr = ptr.next; // ptr에 마지막 노드 저장
                }
                ptr.next = newnode;
            }
            public void listPrint() // 리스트의 노드들을 출력
            {
                for (Node ptr = head; ptr != null; ptr = ptr.next)
                {
                    Console.Write(ptr.key + " ");
                }
                Console.WriteLine();
            }
        }
        class Stack
        {
            private LinkedList list = new LinkedList();

            public void push(int key)
            {
                list.addHead(key);
            }
            public int pop()
            {
                int tmp = list.getHead();
                list.removeHead();
                return tmp;
            }
            public void Sprint()
            {
                list.listPrint();
            }
        }
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();
            list.addHead(4);
            list.addTail(8);
            list.addTail(1);
            list.addTail(5);
            list.addHead(9);
            list.listPrint();

            Stack stack = new Stack();
            stack.push(1);
            stack.push(2);
            stack.push(3);
            stack.push(4);
            stack.Sprint();
            Console.WriteLine("pop", stack.pop());
            Console.WriteLine("pop", stack.pop());
            stack.Sprint();
        }
    }
}