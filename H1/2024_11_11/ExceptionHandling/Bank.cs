class Bank(Dictionary<uint, Account> accounts)
{
    private Dictionary<uint, Account> m_accounts = accounts;
    public uint AddAccount(string fornavn, string efternavn, Admin admin)
    {
        var id = (uint)this.m_accounts.Count;
        this.m_accounts.Add(id, new Account(new Owner(fornavn, efternavn, id), admin));
        return id;
    }
    public void DeleteAccount(uint id)
    {
        this.m_accounts.Remove(id);
    }
    public float UpdateAccountBallance(uint id, float value)
    {
        var account = this.m_accounts[id];
        if (value < 0 && account.m_ballance > value)
            throw new InvalidOperationException();
        account.m_ballance += value;
        return account.m_ballance;
    }
    public float Withdraw(uint id, float value)
    {
        var account = this.m_accounts[id];
        if (account.m_ballance < value)
            throw new InvalidOperationException("Value annot be bigger than ballance");
        account.m_ballance += value;
        return account.m_ballance;

    }
    public float Deposit(uint id, float value)
    {
        var account = this.m_accounts[id];
        if (value < 0)
            throw new ArgumentException("Value cannot be less than 0");
        account.m_ballance += value;
        return account.m_ballance;
    }
}
