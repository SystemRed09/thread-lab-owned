# Reflection

Keep each answer short. Three or four sentences is plenty. What is being assessed is
your reasoning, not your word count.

## 1. Exclusive access (Threads.ReadWrite)

**What went wrong before your fix?** Describe the failure in terms of what two
threads were doing at the same moment, not just "it didn't work."

**Why is the state variable alone not enough?** Name the exact instruction where a
thread can be paused, and what the other thread does while it is asleep.

**What did you lock on, and why not `this`?**

## 2. The race (Threads.Race)

**Which single line in `TokenObject` is not atomic, and what steps does it break into?**

**Why does the total handed out come out too high rather than too low?**

## 3. Deadlock (Threads.Deadlock)

**Which of the four conditions did you break, and how?**

**What does your fix cost?**

Every way of preventing a deadlock takes something away from you somewhere else.
That is what makes this a design decision rather than a repair, and it is why we are
asking.

To be clear about what we mean by *cost*: we are not asking what was difficult to
write, or how long it took you. We are asking what your program can no longer do, or
can no longer promise, now that your fix is in place. One useful way in is to imagine
a colleague joining the project next semester and adding a third job that also needs
two files. What would they have to know, remember, or give up in order to add it
without bringing the deadlock back? Whatever that turns out to be, it is the price
your design is charging.

Answer for the fix you actually wrote, in your own words.

**When would the other option have been the better choice?**

## 4. Overall

**Your fix in exercise 1 is what made exercise 3 possible.** What does that suggest
about where synchronisation decisions belong in a design — in the classes that hold
data, in the code that uses them, or somewhere else?
