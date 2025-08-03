import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { accountsAPI } from '../services/api';
import DepositWithdraw from '../components/DepositWithdraw';

const Dashboard = () => {
  const navigate = useNavigate();
  const { user, logout, isAuthenticated } = useAuth();
  
  // Component state
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  // Redirect if not authenticated
  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login');
    }
  }, [isAuthenticated, navigate]);

  // Fetch accounts on component mount
  useEffect(() => {
    if (isAuthenticated) {
      fetchAccounts();
    }
  }, [isAuthenticated]);

  // Function to fetch user accounts
  const fetchAccounts = async () => {
    try {
      setIsLoading(true);
      setError('');
      
      const response = await accountsAPI.getAccounts();
      setAccounts(response);
      
      // Auto-select first account if available
      if (response && response.length > 0) {
        setSelectedAccount(response[0]);
      }
    } catch (error) {
      console.error('Error fetching accounts:', error);
      setError('Failed to load accounts. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  // Handle account selection
  const handleAccountSelect = (account) => {
    setSelectedAccount(account);
  };

  // Handle logout
  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  // Handle transaction completion - refresh accounts
  const handleTransactionComplete = () => {
    fetchAccounts();
  };

  // Calculate total balance across all accounts
  const totalBalance = accounts.reduce((sum, account) => sum + (account.balance || 0), 0);

  if (!isAuthenticated) {
    return null; // Will redirect via useEffect
  }

  return (
    <div style={styles.container}>
      {/* Header */}
      <header style={styles.header}>
        <h1 style={styles.headerTitle}>Banking Dashboard</h1>
        <div style={styles.headerRight}>
          <span style={styles.welcomeText}>
            Welcome, {user?.username || 'User'}!
          </span>
          <button onClick={handleLogout} style={styles.logoutButton}>
            Logout
          </button>
        </div>
      </header>

      {/* Main Content */}
      <main style={styles.main}>
        {/* Account Summary */}
        <section style={styles.summarySection}>
          <h2 style={styles.sectionTitle}>Account Summary</h2>
          <div style={styles.summaryCard}>
            <div style={styles.summaryItem}>
              <span style={styles.summaryLabel}>Total Accounts:</span>
              <span style={styles.summaryValue}>{accounts.length}</span>
            </div>
            <div style={styles.summaryItem}>
              <span style={styles.summaryLabel}>Total Balance:</span>
              <span style={styles.summaryValue}>${totalBalance.toFixed(2)}</span>
            </div>
          </div>
        </section>

        {/* Error Display */}
        {error && (
          <div style={styles.errorMessage}>
            {error}
            <button onClick={fetchAccounts} style={styles.retryButton}>
              Retry
            </button>
          </div>
        )}

        {/* Loading State */}
        {isLoading && (
          <div style={styles.loadingMessage}>
            Loading your accounts...
          </div>
        )}

        {/* Accounts List */}
        {!isLoading && accounts.length > 0 && (
          <section style={styles.accountsSection}>
            <h2 style={styles.sectionTitle}>Your Accounts</h2>
            <div style={styles.accountsList}>
              {accounts.map((account) => (
                <div
                  key={account.id}
                  style={{
                    ...styles.accountCard,
                    ...(selectedAccount?.id === account.id ? styles.accountCardSelected : {})
                  }}
                  onClick={() => handleAccountSelect(account)}
                >
                  <div style={styles.accountHeader}>
                    <h3 style={styles.accountTitle}>
                      {account.accountType || 'Account'} 
                      {account.accountNumber && ` - ${account.accountNumber}`}
                    </h3>
                    <span style={styles.accountBalance}>
                      ${account.balance?.toFixed(2) || '0.00'}
                    </span>
                  </div>
                  <p style={styles.accountDetails}>
                    Account ID: {account.id}
                  </p>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* No Accounts State */}
        {!isLoading && accounts.length === 0 && !error && (
          <div style={styles.noAccountsMessage}>
            <h3>No accounts found</h3>
            <p>Contact your bank to set up your first account.</p>
          </div>
        )}

        {/* Transaction Section */}
        {selectedAccount && (
          <section style={styles.transactionSection}>
            <DepositWithdraw 
              account={selectedAccount} 
              onTransactionComplete={handleTransactionComplete}
            />
          </section>
        )}
      </main>
    </div>
  );
};

const styles = {
  container: {
    minHeight: '100vh',
    backgroundColor: '#f8f9fa'
  },
  header: {
    backgroundColor: '#343a40',
    color: 'white',
    padding: '20px',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
  },
  headerTitle: {
    margin: 0,
    fontSize: '24px'
  },
  headerRight: {
    display: 'flex',
    alignItems: 'center',
    gap: '15px'
  },
  welcomeText: {
    fontSize: '16px'
  },
  logoutButton: {
    backgroundColor: '#dc3545',
    color: 'white',
    border: 'none',
    padding: '8px 16px',
    borderRadius: '4px',
    cursor: 'pointer',
    fontSize: '14px'
  },
  main: {
    padding: '30px',
    maxWidth: '1200px',
    margin: '0 auto'
  },
  summarySection: {
    marginBottom: '30px'
  },
  sectionTitle: {
    color: '#495057',
    marginBottom: '15px',
    fontSize: '20px'
  },
  summaryCard: {
    backgroundColor: 'white',
    padding: '20px',
    borderRadius: '8px',
    boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
    display: 'flex',
    gap: '40px'
  },
  summaryItem: {
    display: 'flex',
    flexDirection: 'column',
    gap: '5px'
  },
  summaryLabel: {
    color: '#6c757d',
    fontSize: '14px'
  },
  summaryValue: {
    color: '#495057',
    fontSize: '24px',
    fontWeight: 'bold'
  },
  accountsSection: {
    marginBottom: '30px'
  },
  accountsList: {
    display: 'grid',
    gap: '15px',
    gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))'
  },
  accountCard: {
    backgroundColor: 'white',
    padding: '20px',
    borderRadius: '8px',
    boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
    cursor: 'pointer',
    transition: 'all 0.3s ease',
    border: '2px solid transparent'
  },
  accountCardSelected: {
    borderColor: '#007bff',
    boxShadow: '0 4px 8px rgba(0,123,255,0.3)'
  },
  accountHeader: {
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: '10px'
  },
  accountTitle: {
    margin: 0,
    color: '#495057',
    fontSize: '18px'
  },
  accountBalance: {
    color: '#28a745',
    fontSize: '20px',
    fontWeight: 'bold'
  },
  accountDetails: {
    margin: 0,
    color: '#6c757d',
    fontSize: '14px'
  },
  transactionSection: {
    marginTop: '30px'
  },
  errorMessage: {
    backgroundColor: '#f8d7da',
    color: '#721c24',
    padding: '15px',
    borderRadius: '4px',
    marginBottom: '20px',
    border: '1px solid #f5c6cb',
    display: 'flex',
    justifyContent: 'space-between',
    alignItems: 'center'
  },
  retryButton: {
    backgroundColor: '#007bff',
    color: 'white',
    border: 'none',
    padding: '5px 10px',
    borderRadius: '4px',
    cursor: 'pointer',
    fontSize: '12px'
  },
  loadingMessage: {
    textAlign: 'center',
    padding: '40px',
    color: '#6c757d',
    fontSize: '18px'
  },
  noAccountsMessage: {
    textAlign: 'center',
    padding: '40px',
    backgroundColor: 'white',
    borderRadius: '8px',
    boxShadow: '0 2px 4px rgba(0,0,0,0.1)'
  }
};

export default Dashboard;