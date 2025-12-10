# Committing changes

Follow these steps to commit your work:

1. **Check your git status** to see modified files:
   ```bash
   git status -sb
   ```
2. **Stage files** you want to include in the commit:
   ```bash
   git add <file1> <file2>
   ```
   To stage all tracked changes at once, you can use:
   ```bash
   git add -A
   ```
3. **Verify staged content** (optional but recommended):
   ```bash
   git diff --cached
   ```
4. **Create the commit** with a concise message:
   ```bash
   git commit -m "Your message here"
   ```
5. **Review the commit log** to confirm it was recorded:
   ```bash
   git log --oneline -n 5
   ```

If you need to amend the most recent commit (for example, to adjust the message or add a forgotten file), use:
```bash
git commit --amend
```

To push your commit to the remote `work` branch:
```bash
git push origin work
```
