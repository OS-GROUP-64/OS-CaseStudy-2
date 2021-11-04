# Case Study #2 HW

## Problems
ให้ศึกษาโปรแกรมที่กำหนดให้
ให้แก้ไขดัดแปลงโปรแกรมที่กำหนดให้มีคุณสมบัติดังนี้
- ทำงานได้อย่างถูกต้องกับ Queue Buffer 10 slots
- ทำงานกับ Thread ได้อย่างถูกต้อง (Thread safe)

## Summition
source code ของ version ที่ นศ คิดว่าเป็น version ที่ดีที่สุด
สรุปสิ่งที่สังเกตพบ ปัญหา วิธีแก้ ลงในไฟล์ pdf และตั้งชื่อไฟล์โดยใช้รหัสของสมาชิกคั่นด้วยเครื่องหมาย _ เช่น 62010001_62010002_62010003.pdf **ให้เรียงลำดับรหัสจากน้อยไปหามากด้วย**
ตัวแทนกลุ่มเป็นผู้ส่งเพียงผู้เดียวสำหรับกลุ่มหนึ่งๆ
ใส่ชื่อสมาชิกกลุ่มในไฟล์ให้เรียบร้อย

## Installation
<!-- How to install this project -->

1. Clone this project

## Issues and Resolves

1. V1.0 
Base code: ปัญหามี 2 Threads เป็น EnQueue และ DeQueue และมี Buffer อยู่เพียง 10 ช่อง และโปรแกรมมีการเพิ่มค่าเข้าไปใน Buffer เกินกว่า 10 ค่า การทำงานจึงไม่ถูกต้อง เนื่องจาก EnQueue และ DeQueue ทำงานพร้อมกัน แต่ใช้เวลาในการทำงานต่างกัน (5ms และ 100ms ตามลำดับ) ทำให้ลำดับการนำค่าออกมาจาก Buffer ผิดไปและไม่ถูกต้องตามลำดับ 

2. V1.5
Problem: (From V1.0) ปัญหากับ EnQueue และ DeQueue ที่มี Buffer เพียงแค่ 10 ช่อง แต่ข้อมูลที่ป้อนมีมากกว่านั้น
Solved: ลองใช้ Conditional Variable ใน C# จะเป็น Monitor.Wait(), Monitor.Pulse() ซึ่งเราได้ใช้ Monitor.Wait(_Lock); ใน Thread EnQueue และ ใช้ Monitor.pulse(_Lock); ใน Thread DeQueue และใช้ตัวแปร Count ในการกำหนดเงื่อนไขในการทำงานของ EnQueue และ DeQueue
Result: โปรแกรมมีการทำงานที่ผิดพลาด, มีการรันอย่างต่อเนื่องหลังจากส่ง Pulse ไป ใน Thread DeQueue ทำให้ DeQueue ไม่รอและทำต่อเนื่องไป คาดว่า เกิดจากการที่มีแค่ Wait หรือ Pulse ใน แต่ละ Thread และยังไม่เข้าใจการทำงานของฟังชั่นนี้มากพอ

3. V2.0
Problem: (From V1.5) ปัญหากับ EnQueue และ DeQueue ที่มี Buffer เพียงแค่ 10 ช่อง แต่ข้อมูลที่ป้อนมีมากกว่านั้น และความเข้าใจฟังชั่น Wait และ Pulse
Solved: เราได้ศึกษาเพิ่มเติมถึงวิธีการเขียน Monitor.Wait() และ Monitor.Pulse() เพิ่มเติม และดูวิธีการใช้งาน พบว่า ใน 1 Thread ควรจะต้องมีทั้ง Wait และ Pulse เพื่อส่งสัญญาณให้อีก Thread ทำงาน และให้ Thread ตัวเองหยุดอยู่เมื่อเข้าเงื่อนไขที่เรากำหนด โดยใช้ตัวแปร Count
Result: ทำงานได้ถูกต้อง ผลลัพท์เป็นไปตาม Sequence ที่เราต้องการ Threads ทั้งสองมีการรอกันและถูกปลุกอย่างถูกต้องตามเงื่อนไขที่เรากำหนด

Reference:
- [Creating a blocking Queue<T> in .NET?](https://stackoverflow.com/questions/530211/creating-a-blocking-queuet-in-net/530228#530228r)