# C#으로 STACK 구현 ( 배열 & 링크드리스트)

## 목차
1. [배열을 이용한 STACK 구현](#1-배열을-이용한-STACK-구현)
2. [링크드리스트를 이용한 STACK 구현](#2-링크드리스트를-이용한-STACK-구현)
---

### 1. 배열을 이용한 STACK 구현
```cs
using System;

namespace Stack_report
{
    internal class Program1
    {
        static void Main(string[] args)
        {
            char[] parking = new char[5]; // parking 배열 생성
            int top = 0; // 스택의 맨 위 (배열의 첫번째)

            parking[top] = 'A'; // 스택의 맨 위에 데이터 추가
            Console.WriteLine("{0} 자동차가 주차장에 들어감", parking[top]);
            top++; // 배열의 다음칸으로 이동 (스택 쌓기 위해 한칸 위로)

            parking[top] = 'B';
            Console.WriteLine("{0} 자동차가 주차장에 들어감", parking[top]);
            top++;

            parking[top] = 'C';
            Console.WriteLine("{0} 자동차가 주차장에 들어감", parking[top]);
            top++;

            Console.WriteLine();

            top--; // 스택의 맨 위에 있는 데이터 삭제
            Console.WriteLine("{0} 자동차가 주차장을 빠져나감", parking[top]);
            parking[top] = ' '; // 해당 스택 위치에 값 삭제

            top--;
            Console.WriteLine("{0} 자동차가 주차장을 빠져나감", parking[top]);
            parking[top] = ' ';

            top--;
            Console.WriteLine("{0} 자동차가 주차장을 빠져나감", parking[top]);
            parking[top] = ' ';
        }
    }
}

```
![array](https://user-images.githubusercontent.com/32214586/227764350-656548a7-3a4f-40ea-9d6f-f46381abde66.png)
### 2. 링크드리스트를 이용한 STACK 구현
```cs
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

            public void push(int key) // key값을 push
            {
                list.addHead(key);
            }
            public int pop() // head의 값을 tmp에 저장 후 지우고 반환
            {
                int tmp = list.getHead();
                list.removeHead();
                return tmp;
            }
            public void Sprint() // 현재 스택 출력
            {
                list.listPrint();
            }
        }
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList(); // 링크드리스트 이용 head & tail 추가
            list.addHead(4);
            list.addTail(8);
            list.addTail(1);
            list.addTail(5);
            list.addHead(9);
            list.listPrint();

            Stack stack = new Stack(); // 스택 이용 데이터 push & pop
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
```
![Linkedlist](https://user-images.githubusercontent.com/32214586/227767463-976989e2-b6fd-41d9-9f7a-c1bcf563d103.png)
