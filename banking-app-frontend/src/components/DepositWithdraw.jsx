import React, { useState } from 'react';
import { accountsAPI } from '../services/api';

const DepositWithdraw = ({ account, onTransactionComplete }) => {
  const [amount, setAmount] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  // Validate amount input
  const validateAmount = (value) => {
    const numValue = parseFloat(value);
    if (isNaN(numValue) || numValue <= 0) {
      setError('Please enter a valid amount greater than 0');
      return false;
    }
    if (numValue > 10000) {
      setError('Maximum transaction amount is $10,000');
      return false;
    }
    return true;
  };

  // Handle input change
  const handleAmountChange = (e) => {
    const value = e.target.value;
    setAmount(value);
    
    // Clear messages when typing
    if (error) setError('');
    if (success) setSuccess('');
  };

  // Handle deposit
  const handleDeposit = async () => {
    if (!validateAmount(amount)) {
      return;
    }

    setIsLoading(true);
    setError('');
    setSuccess('');

    try {
      const result = await accountsAPI.deposit(account.id, parseFloat(amount));
      setSuccess(`Successfully deposited $${amount}`);
      setAmount('');
      
      // Notify parent component to refresh account data
      if (onTransactionComplete) {
        onTransactionComplete();
      }
    } catch (error) {
      console.error('Deposit error:', error);
      setError(error.response?.data?.message || 'Deposit failed. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  // Handle withdrawal
  const handleWithdraw = async () => {
    if (!validateAmount(amount)) {
      return;
    }

    // Additional validation for withdrawal
    const withdrawAmount = parseFloat(amount);
    if (withdrawAmount > account.balance) {
      setError('Insufficient funds for this withdrawal');
      return;
    }

    setIsLoading(true);
    setError('');
    setSuccess('');

    try {
      const result = await accountsAPI.withdraw(account.id, withdrawAmount);
      setSuccess(`Successfully withdrew $${amount}`);
      setAmount('');
      
      // Notify parent component to refresh account data
      if (onTransactionComplete) {
        onTransactionComplete();
      }
    } catch (error) {
      console.error('Withdrawal error:', error);
      setError(error.response?.data?.message || 'Withdrawal failed. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div style={styles.container}>
      <h3 style={styles.title}>Account Transactions</h3>
      
      <div style={styles.accountInfo}>
        <p><strong>Account:</strong> {account.accountNumber || account.id}</p>
        <p><strong>Current Balance:</strong> ${account.balance?.toFixed(2) || '0.00'}</p>
      </div>

      {error && (
        <div style={styles.errorMessage}>
          {error}
        </div>
      )}

      {success && (
        <div style={styles.successMessage}>
          {success}
        </div>
      )}

      <div style={styles.transactionForm}>
        <div style={styles.inputGroup}>
          <label style={styles.label}>Amount:</label>
          <input
            type="number"
            value={amount}
            onChange={handleAmountChange}
            placeholder="Enter amount"
            style={styles.input}
            min="0.01"
            max="10000"
            step="0.01"
            disabled={isLoading}
          />
        </div>

        <div style={styles.buttonGroup}>
          <button
            onClick={handleDeposit}
            disabled={isLoading || !amount}
            style={{
              ...styles.button,
              ...styles.depositButton,
              ...(isLoading ? styles.buttonDisabled : {})
            }}
          >
            {isLoading ? 'Processing...' : 'Deposit'}
          </button>

          <button
            onClick={handleWithdraw}
            disabled={isLoading || !amount}
            style={{
              ...styles.button,
              ...styles.withdrawButton,
              ...(isLoading ? styles.buttonDisabled : {})
            }}
          >
            {isLoading ? 'Processing...' : 'Withdraw'}
          </button>
        </div>
      </div>
    </div>
  );
};

const styles = {
  container: {
    backgroundColor: '#f8f9fa',
    padding: '20px',
    borderRadius: '8px',
    marginTop: '20px',
    border: '1px solid #dee2e6'
  },
  title: {
    margin: '0 0 15px 0',
    color: '#495057',
    fontSize: '18px'
  },
  accountInfo: {
    backgroundColor: 'white',
    padding: '15px',
    borderRadius: '4px',
    marginBottom: '20px',
    border: '1px solid #dee2e6'
  },
  transactionForm: {
    display: 'flex',
    flexDirection: 'column',
    gap: '15px'
  },
  inputGroup: {
    display: 'flex',
    flexDirection: 'column',
    gap: '5px'
  },
  label: {
    fontWeight: 'bold',
    color: '#495057'
  },
  input: {
    padding: '10px',
    border: '1px solid #ced4da',
    borderRadius: '4px',
    fontSize: '16px'
  },
  buttonGroup: {
    display: 'flex',
    gap: '10px',
    justifyContent: 'space-between'
  },
  button: {
    flex: 1,
    padding: '12px',
    border: 'none',
    borderRadius: '4px',
    fontSize: '16px',
    fontWeight: 'bold',
    cursor: 'pointer',
    transition: 'background-color 0.3s'
  },
  depositButton: {
    backgroundColor: '#28a745',
    color: 'white'
  },
  withdrawButton: {
    backgroundColor: '#dc3545',
    color: 'white'
  },
  buttonDisabled: {
    backgroundColor: '#6c757d',
    cursor: 'not-allowed'
  },
  errorMessage: {
    backgroundColor: '#f8d7da',
    color: '#721c24',
    padding: '10px',
    borderRadius: '4px',
    marginBottom: '15px',
    border: '1px solid #f5c6cb'
  },
  successMessage: {
    backgroundColor: '#d4edda',
    color: '#155724',
    padding: '10px',
    borderRadius: '4px',
    marginBottom: '15px',
    border: '1px solid #c3e6cb'
  }
};

export default DepositWithdraw;