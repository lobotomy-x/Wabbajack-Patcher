# Problem

The popular modding software Wabbajack has major issues stemming from an intentional design decision to prevent running the program as admin. This makes it entirely impossible to use the software on admin accounts if user account control is disabled. This is not a concern for most users however there are many additional problems that can occur, often without the user noticing or understanding them. These are typically related to access rights issues with the filesystem and with external processes launched by wabbajack (they could ask for elevation but they do not). The end result is typically a failed installation but it is possible for an install to look like it worked correctly but have issues with corrupted files or failed patchers. The developers do not acknowledge the real issues they've caused and tend to belittle their users who raise valid complaints so its safe to say we cannot pull request a fix.

# Solution 1: Fork the Software

[This was done previously but updates are too frequent and sometimes too large to maintain](https://github.com/lobotomy-x/betterwabbajack)

# Solution 2: Use DNSpy

Since Wabbajack is written in C# we can easily use the software DNSpy to decompile the assemblies and manually patch the target. That works great but it doesn't really solve the issue of updates.

# Solution 3: Make an Automated Patcher

Using dnlib, a nuget package used by DNSpy we can handle the patch programmatically. This has worked for at least one year at the time of writing and it will probably continue to work indefinitely. Consider this a simple working example of a dnlib patcher.

For Wabbajack users, download the release or compile the program. If you download the release you will need the NET 9.0 runtime. Run it from the same directory as wabbajack and it will automatically patch the program. After that you can run wabbajack as admin.
